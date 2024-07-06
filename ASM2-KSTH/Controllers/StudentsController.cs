using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASM2_KSTH.Data;
using ASM2_KSTH.Models;
using Microsoft.AspNetCore.Authorization;
using ASM2_KSTH.ViewModels;
using System.Diagnostics;
using ASM2_KSTH.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASM2_KSTH.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ASM2_KSTHContext _context;

        public StudentsController(ASM2_KSTHContext context)
        {
            _context = context;
        }

        #region Login for Student
        
        // GET: Students
        [HttpGet]
        public IActionResult Index(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            ViewData["ReturnUrl"] = ReturnUrl;
            return View();
        }


        // POST: Students
        [HttpPost]
        public async Task<IActionResult> Index(Student model, string? ReturnUrl)
        {
                ViewBag.ReturnUrl = ReturnUrl;
                // Thực hiện xác thực thông tin đăng nhập tại đây
                var student =  _context.Students.SingleOrDefault(u => u.Username == model.Username);
                if (student == null || student.Password != model.Password.ToMd5Hash(student.RandomKey))
                {
                    // Xác thực thất bại, đặt thông báo lỗi vào ViewBag và hiển thị lại form đăng nhập
                    ViewBag.ErrorMessage = "Invalid username or password.";
               
                return View("Index", model);

                }
                else
                {
                    var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == student.RoleId);
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, student.StudentId.ToString()),
                        new Claim(ClaimTypes.Name, student.Username),
                        new Claim(ClaimTypes.Role, "Students"),
                        new Claim("FullName", student.Name),
                        new Claim("Email", student.Email ?? string.Empty),

                    };
                    Console.WriteLine(student);
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(claimsPrincipal);
                     TempData["ok"] = "Student registered successfully!";

                if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("DashBoard", "Students");
                    }
                }

            return View();
        }
		#endregion

		[Authorize(Roles = "Students")]
		#region Change password for Student
		[HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
       
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword, string confirmNewPassword)
        {
            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmNewPassword))
            {
				TempData["no"] = "Please fill in all required fields.";
                return View();
            }

            // Get the currently logged-in user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("Index", "Home"); // Or another action for unauthorized access
            }

            var student = await _context.Students.FindAsync(int.Parse(userId));
            if (student == null)
            {
                return NotFound();
            }

            // Validate old password
            if (student.Password != oldPassword.ToMd5Hash(student.RandomKey))
            {
				TempData["no"] = "The old password is incorrect.";
                return View();
            }

            // Validate new password and confirmation
            if (newPassword != confirmNewPassword)
            {
				TempData["no"] = "The new password and confirmation password do not match.";
                return View();
            }

            // Update password
            student.Password = newPassword.ToMd5Hash(student.RandomKey);
            _context.Update(student);
            await _context.SaveChangesAsync();

            TempData["ok"] = "Password changed successfully!";
            return RedirectToAction("DashBoard", "Students");
        }

        #endregion

        #region Profile for Student

        [Authorize(Roles = "Students")]
        [HttpGet]
        public async Task<IActionResult> ProfileST()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("Index", "Students");
            }

            var studentId = int.Parse(userId);
            var student = await _context.Students
                .Include(s => s.Major)
                .FirstOrDefaultAsync(s => s.StudentId == studentId);

            if (student == null)
            {
                return NotFound();
            }

            var model = new StudentRegister
            {
                StudentId = student.StudentId,
                Name = student.Name,
                DateOfBirth = student.DateOfBirth,
                Address = student.Address ?? string.Empty,
                PhoneNumber = student.PhoneNumber,
                Email = student.Email ?? string.Empty,
                MajorId = student.MajorId,
                MajorName = student.Major?.MajorName ?? string.Empty,
                Username = student.Username,
            };
            ViewBag.StudentName = student.Name;
            ViewBag.StudentEmail = student.Email;

            ViewBag.MajorName = new SelectList(_context.Majors, "MajorName", "MajorName", model.MajorName);
            ViewBag.RoleId = new SelectList(_context.Roles, "RoleId", "RoleName", student.RoleId);

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProfileST(StudentRegister model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // lấy dữ liệu thông tin khi đăng nhập thành công
            if (userId == null)
            {
                return RedirectToAction("Index", "Students");
            }

            var studentId = int.Parse(userId);
            if (studentId != model.StudentId)
            {
                return NotFound();
            }
            try
            {
                // Gọi chay dữ liệu (điều kiện khi một trường có giá trị null)
                var student = await _context.Students
                    .Include(s => s.Roles)
                    .FirstOrDefaultAsync(s => s.StudentId == studentId);

                if (student == null)
                {
                    return NotFound();
                }

                // Tìm MajorId dựa trên MajorName
                var major = await _context.Majors.FirstOrDefaultAsync(m => m.MajorName == model.MajorName);
                if (major == null)
                {
                    ModelState.AddModelError("", "Invalid major selected.");
                    ViewBag.MajorName = new SelectList(_context.Majors, "MajorName", "MajorName", model.MajorName);
                    ViewBag.RoleId = new SelectList(_context.Roles, "RoleId", "RoleName", model.RoleId);
                    return View(model);
                }

                student.Name = model.Name;
                student.DateOfBirth = model.DateOfBirth;
                student.Address = model.Address ?? string.Empty; // Xử lý giá trị null
                student.PhoneNumber = model.PhoneNumber;
                student.Email = model.Email ?? string.Empty;     // Xử lý giá trị null
                student.MajorId = major.MajorId; // Cập nhật MajorId dựa trên MajorName

                _context.Update(student);
                await _context.SaveChangesAsync();
                TempData["ok"] = "Edit Profile Student Successful !";

                ViewBag.StudentName = student.Name;
                ViewBag.StudentEmail = student.Email;

                return RedirectToAction("DashBoard", "Students");
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "Unable to save changes. The student was updated or deleted by another user.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while saving changes: {ex.Message}");
            }
            // Nếu có lỗi xảy ra, cần điền lại dữ liệu cho model để hiển thị lại view
            ViewBag.MajorName = new SelectList(_context.Majors, "MajorName", "MajorName", model.MajorName);
            ViewBag.RoleId = new SelectList(_context.Roles, "RoleId", "RoleName", model.RoleId);

            return View(model);
        }



        #endregion

        [Authorize(Roles = "Students")]
        public async Task<IActionResult> ListStudentinClass(int studentId)
        {
            // Find the student by StudentId
            var student = await _context.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Class)
                .ThenInclude(c => c.Course)
                .FirstOrDefaultAsync(s => s.StudentId == studentId);

            if (student == null)
            {
                return NotFound();
            }

            // Get all class IDs for the student's enrollments
            var classIds = student.Enrollments.Select(e => e.ClassId).ToList();

            // Get all students in those classes
            var studentsInClasses = await _context.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Class)
                .ThenInclude(c => c.Course)
                .Where(s => s.Enrollments.Any(e => classIds.Contains(e.ClassId)))
                .ToListAsync();

            // Prepare the view model for displaying students in class
            var studentInClassViewModels = studentsInClasses.Select(s => new StudentInClassViewModel
            {
                StudentId = s.StudentId,
                StudentName = s.Name,
                ClassId = s.Enrollments.FirstOrDefault(e => classIds.Contains(e.ClassId)).ClassId,
                ClassName = s.Enrollments.FirstOrDefault(e => classIds.Contains(e.ClassId)).Class.ClassName,
                CourseId = s.Enrollments.FirstOrDefault(e => classIds.Contains(e.ClassId)).Class.CourseId.Value,
                CourseName = s.Enrollments.FirstOrDefault(e => classIds.Contains(e.ClassId)).Class.Course.CourseName
            }).ToList();

            ViewBag.StudentName = student.Name;
            ViewBag.ClassName = student.Enrollments.FirstOrDefault(e => classIds.Contains(e.ClassId)).Class.ClassName;

            return View(studentInClassViewModels);
        }


        [Authorize(Roles = "Students")]
        public async Task<IActionResult> CheckAttendance()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("Index", "Students");
            }

            var studentId = int.Parse(userId);
            var attendanceStatuses = await _context.Attendance
                .Where(a => a.StudentId == studentId)
                .Select(a => new AttendanceStatusViewModel
                {
                    ClassId = a.ClassId,
                    ClassName = a.Class.ClassName,
                    CourseName = a.Class.Course.CourseName,
                    AttendanceStatus = a.AttendanceStatus,
                    Numses = a.Class.Course.NumSessions.FirstOrDefault(ns => ns.NumId == a.NumId).Numses,
                    Reason = a.Reason,
                    AttendanceDate = a.AttendanceDate.ToDateTime(new TimeOnly(0, 0))
                })
                .ToListAsync();

            return View(attendanceStatuses);
        }


        [Authorize(Roles = "Students")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            TempData["ok"] = "See You Again !";
            return Redirect("/");
        }

       
        public async Task<IActionResult> Dashboard()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var studentId = int.Parse(userId);
            var studentName = await _context.Students
                                .Where(a => a.StudentId == studentId)
                                .Select(a => a.Name)
                                .FirstOrDefaultAsync();
            ViewBag.StudentName = studentName;
            return View();
        }
    }
}
