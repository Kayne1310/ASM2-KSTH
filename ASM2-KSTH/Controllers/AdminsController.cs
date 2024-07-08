
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASM2_KSTH.Data;
using ASM2_KSTH.Models;
using Microsoft.AspNetCore.Authorization;
using ASM2_KSTH.ViewModels;
using AutoMapper;
using ASM2_KSTH.Helpers;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;
using NuGet.DependencyResolver;
using System.Linq;


namespace ASM2_KSTH.Controllers
{
    public class AdminsController : Controller
    {
        private readonly ASM2_KSTHContext _context;
        private readonly IMapper _mapper;

        public AdminsController(ASM2_KSTHContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Login for Admin
       
        // GET: Admins
        [HttpGet]
        public IActionResult Index(string? ReturnUrl)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            return View();
        }

        [Authorize(Roles = "Admins")]
        public IActionResult AdminPage()
        {
            return View();
        }

        public async Task<IActionResult> Dashboard()
        {
            ViewBag.TeacherCount = await _context.Teachers.CountAsync();
            ViewBag.StudentCount = await _context.Students.CountAsync();
            ViewBag.ClassCount = await _context.Classes.CountAsync();
            ViewBag.RoomCount = await _context.Rooms.CountAsync();
            ViewBag.CourseCount = await _context.Courses.CountAsync();
            return View();
        }
    


        public IActionResult ListST()
        {
            return View();
        }



        // POST: Admins
        [HttpPost]
        public async Task<IActionResult> Index(Admin model, string? ReturnUrl)
        {
     
                ViewBag.ReturnUrl = ReturnUrl;
                // Thực hiện xác thực thông tin đăng nhập tại đây
                var admin = await _context.Admins.FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password); 
                
                if (admin == null)
                {
                    // Xác thực thất bại, đặt thông báo lỗi vào ViewBag và hiển thị lại form đăng nhập
                    ViewBag.ErrorMessage = "Invalid username or password.";
                
                   return View("Index", model);

                }
                else
                {
                    var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == admin.RoleId);
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Role, "Admins"),
                        new Claim(MySetting.CLAIM_ID, admin.Username),

                    };
              
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(claimsPrincipal);
                TempData["ok"] = "Welcome Back Admin!";

                if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("DashBoard", "Admins");
                    }
                }
                return View();
        }
        #endregion

        #region Register for Student
        [Authorize(Roles = "Admins")]
        [HttpGet]
        public IActionResult SignupST(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            ViewBag.Majors = _context.Majors.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult SignupST(StudentRegister model, string? ReturnUrl)
        {
            try
            {
                ViewBag.ReturnUrl = ReturnUrl;
                ViewBag.Majors = _context.Majors.ToList();
                var existingStudent = _context.Students.FirstOrDefault(t => t.Username == model.Username);
                if (existingStudent != null)
                {
                    // Nếu username đã tồn tại, hiển thị thông báo lỗi và trả về View
                    ViewBag.ErrorMessage = "Username already exists. Please choose a different username.";
                    TempData["no"]="Username already exists. Please choose a different username.";
                   
                    return View();
                }
                // Truy vấn cơ sở dữ liệu để lấy MajorId dựa trên MajorName
                var major = _context.Majors.FirstOrDefault(m => m.MajorName == model.MajorName);
                if (major == null)
                {
                    // Xử lý trường hợp không tìm thấy MajorName
                    return NotFound();
                }

                // Map dữ liệu từ model sang đối tượng Student
                var student = _mapper.Map<Student>(model);
                student.RandomKey = MyUtil.GenerateRandomKey();
                student.Password = model.Password.ToMd5Hash(student.RandomKey);
                student.RoleId = model.RoleId;
                student.MajorId = major.MajorId;

                // Thêm sinh viên mới vào cơ sở dữ liệu
                _context.Students.Add(student);
                _context.SaveChanges();
                TempData["ok"] = "Create Student Successful!";
                return RedirectToAction("AdminPage", "Admins");
            }

            catch (Exception ex)
            {
                var mess = $"{ex.Message} shh";
                // Có thể thêm logging hoặc xử lý lỗi khác ở đây
            }
            return View();
        }
        #endregion

        #region Register for Teacher
        [Authorize(Roles = "Admins")]
		[HttpGet]
        public IActionResult SignupTE(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            //ViewBag.Teachers = _context.Teachers.ToList();
            return View();
        }


        [HttpPost]
        public IActionResult SignupTE(Teacher model, string? ReturnUrl)
        {
            try
            {
                ViewBag.ReturnUrl = ReturnUrl;
                var existingTeacher = _context.Teachers.FirstOrDefault(t => t.Username == model.Username);
                if (existingTeacher != null)
                {
                    // Nếu username đã tồn tại, hiển thị thông báo lỗi và trả về View
                    ViewBag.ErrorMessage = "Username already exists. Please choose a different username.";
                    TempData["no"] = "Username already exists. Please choose a different username.";
                    return View();
                }
                var teacher = new Teacher
                {             
                    Name = model.Name,  // Example property
                    Address = model.Address,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    RandomKey = model.RandomKey, 
                    Username = model.Username,
                    Password = model.Password,
                    RoleId = model.RoleId,

                };
                teacher.RandomKey = MyUtil.GenerateRandomKey();
                teacher.Password = model.Password.ToMd5Hash(teacher.RandomKey);

                // Thêm giáo viên mới vào cơ sở dữ liệu
                _context.Teachers.Add(teacher);
                _context.SaveChanges();
                TempData["ok"] = "Create Teacher Successfull!";
                return RedirectToAction("AdminPage", "Admins");
            }

            catch (Exception ex)
            {
                var mess = $"{ex.Message} shh";
                // Có thể thêm logging hoặc xử lý lỗi khác ở đây
            }
            return View();
        }



        #endregion

        #region List of Student
        [Authorize(Roles = "Admins")]
		[HttpGet]
		public async Task<IActionResult> ListStudent(int MajorId)
		{
			var students = await _context.Students
				.Include(s => s.Major)
				.Select(e => new StudentRegister
				{
					StudentId = e.StudentId,
					Name = e.Name,
					DateOfBirth = e.DateOfBirth,
					Address = e.Address,
					PhoneNumber = e.PhoneNumber,
					Email = e.Email,
					MajorName = e.Major.MajorName,
				})
				.ToListAsync();
			ViewData["MajorId"] = MajorId;
			return View(students);
		}

		[HttpPost]
		public async Task<IActionResult> ListStudent(int MajorId, string actionType)
		{
			// Kiểm tra actionType để xác định hành động cần thực hiện
			switch (actionType)
			{
				case "list":
					var students = await _context.Students
						.Include(s => s.Major)
						.Where(e => e.MajorId == MajorId)
						.Select(e => new StudentRegister
						{
							StudentId = e.StudentId,
							Name = e.Name,
							DateOfBirth = e.DateOfBirth,
							Address = e.Address,
							PhoneNumber = e.PhoneNumber,
							Email = e.Email,
							MajorName = e.Major.MajorName,
						})
						.ToListAsync();
					    return View("ListStudent", students); // return the view with student list

				        // Thêm các trường hợp khác tương ứng với các actionType khác nhau

				default:
					return BadRequest("Invalid action type"); // return bad request if actionType is not expected
			}
		}
		#endregion

		#region Edit list student
		[Authorize(Roles = "Admins")]
		[HttpGet]
        public async Task<IActionResult> EditListST(int id)
        {
            var student = await _context.Students
                .Include(s => s.Major)
                .Include(s => s.Roles)
                .Select(e => new Student
                   {
                       StudentId = e.StudentId,
                       Name = e.Name,
                       DateOfBirth = e.DateOfBirth,
                       Address = e.Address != null ? e.Address : string.Empty,  // Xử lý giá trị null
                       Email = e.Email != null ? e.Email : string.Empty,        // Xử lý giá trị null
                       PhoneNumber = e.PhoneNumber,
                       Username = e.Username,
                       Password = e.Password,
                       MajorId = e.MajorId,
                   })
                .FirstOrDefaultAsync(s => s.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            var model = new StudentRegister
            {
                StudentId = student.StudentId,
                Name = student.Name,
                DateOfBirth = student.DateOfBirth,
                Address = student.Address,
                PhoneNumber = student.PhoneNumber,
                Email = student.Email,
                MajorId = student.MajorId,
                MajorName = student.Major?.MajorName,

            };

            ViewBag.MajorName = new SelectList(_context.Majors, "MajorName", "MajorName", student.Major?.MajorName);
            ViewBag.RoleId = new SelectList(_context.Roles, "RoleId", "RoleName", student.RoleId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditListST(int id, StudentRegister model)
        {
            if (id != model.StudentId)
            {
                return NotFound();
            }
                try
                {
                var student = await _context.Students // Gọi chay dữ liệu (điều kiện khi 1 trường Null)
                     .Include(s => s.Roles)
                     .Select(e => new Student
                     {
                         StudentId = e.StudentId,
                         Name = e.Name,
                         DateOfBirth = e.DateOfBirth,
                         Address = e.Address != null ? e.Address : string.Empty,  // Xử lý giá trị null
                         Email = e.Email != null ? e.Email : string.Empty,        // Xử lý giá trị null
                         PhoneNumber = e.PhoneNumber,
                         Username = e.Username,
                         Password = e.Password,
                         RandomKey = e.RandomKey,
                         MajorId = e.MajorId,
                         RoleId = e.RoleId,
                     })
                     .FirstOrDefaultAsync(s => s.StudentId == id);
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
                        return View(model);
                    }

                    student.Name = model.Name;
                    student.DateOfBirth = model.DateOfBirth;
                    student.Address = model.Address;
                    student.PhoneNumber = model.PhoneNumber;
                    student.Email = model.Email;
                    student.MajorId = major.MajorId; // Cập nhật MajorId dựa trên MajorName


                    _context.Update(student);
                    await _context.SaveChangesAsync();
                    TempData["ok"] = "Edit Student Successfull!";
    
                     return RedirectToAction("ListStudent","Admins");   
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

		#region Delete All Info Student
		[Authorize(Roles = "Admins")]
        [HttpGet]
        public async Task<IActionResult> DeleteListST(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Roles)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        [HttpPost, ActionName("DeleteListST")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedST(int? id)
		{
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                TempData["error"] = "Student not found!";
                return RedirectToAction("ListStudent", "Admins");
            }
            _context.Students.Remove(student);

            // Remove related enrollment if necessary
            var enrollment = await _context.Enrollments
                .Where(c => c.StudentId == id) // Assuming you have a StudentId field in the enrollment entity
                .ToListAsync();
            if (enrollment != null)
            {
                _context.Enrollments.RemoveRange(enrollment);
            }

            // Find enrollments related to the student and delete associated grades
            var enrollments = await _context.Enrollments
                .Where(e => e.StudentId == id) // Assuming StudentId field exists in Enrollments entity
                .ToListAsync();

            if (enrollments != null && enrollments.Count > 0)
            {
                var enrollmentIds = enrollments.Select(e => e.EnrollmentId).ToList();

                var grades = await _context.Grades
                    .Where(g => enrollmentIds.Contains((int)g.EnrollmentId)) // Assuming EnrollmentId field exists in Grades entity
                    .ToListAsync();

                if (grades != null && grades.Count > 0)
                {
                    _context.Grades.RemoveRange(grades);
                }

                // Optionally remove the enrollments themselves if needed
                _context.Enrollments.RemoveRange(enrollments);
            }
            // Remove related grade if necessary
            var schedule = await _context.Schedules
                .Where(c => c.Enrollments.StudentId == id) // Assuming you have a EnrollmentId field in the grade entity
                .ToListAsync();
            if (schedule != null)
            {
                _context.Schedules.RemoveRange(schedule);
            }

            // Set StudentId to null in related attendance
            var attendance = await _context.Attendance
                .Where(c => c.StudentId == id)
                .ToListAsync();
            if (attendance != null)
            {
                foreach (var attendance1 in attendance)
                {
                    attendance1.StudentId = null;
                }
            }

            await _context.SaveChangesAsync();
            TempData["ok"] = "Delete Student Successfully!";
            return RedirectToAction("ListStudent", "Admins");

        }



		#endregion

		#region List of Teacher
		[Authorize(Roles = "Admins")]
		[HttpGet]
		public async Task<IActionResult> ListTeacher()
		{
			var teachers = await _context.Teachers
				.Select(e => new Teacher
				{
					TeacherId = e.TeacherId,
					Name = e.Name,
                    Address = e.Address != null ? e.Address : string.Empty,  // Xử lý giá trị null
                    Email = e.Email != null ? e.Email : string.Empty,        // Xử lý giá trị null
                    PhoneNumber = e.PhoneNumber,
				})
				.ToListAsync();
			    return View(teachers);
		}

		[HttpPost]
		public async Task<IActionResult> ListTeacher(string actionType)
		{
			// Kiểm tra actionType để xác định hành động cần thực hiện
			switch (actionType)
			{
				case "list":
					var teachers = await _context.Teachers
						.Select(e => new Teacher
						{
							TeacherId = e.TeacherId,
							Name = e.Name,
							Address = e.Address,
							Email = e.Email,
							PhoneNumber = e.PhoneNumber,
						})
						.ToListAsync();
					return View("ListTeacher", teachers); // return the view with student list

				// Thêm các trường hợp khác tương ứng với các actionType khác nhau

				default:
					return BadRequest("Invalid action type"); // return bad request if actionType is not expected
			}
		}
		#endregion

		#region Edit list teacher
		[Authorize(Roles = "Admins")]
		[HttpGet]        
		public async Task<IActionResult> EditListTE(int id)
		{
			var teacher = await _context.Teachers
				.Include(s => s.Roles)
                .Select(e => new Teacher
                {
                    TeacherId = e.TeacherId,
                    Name = e.Name,
                    Address = e.Address != null ? e.Address : string.Empty,  // Xử lý giá trị null
                    Email = e.Email != null ? e.Email : string.Empty,        // Xử lý giá trị null
                    PhoneNumber = e.PhoneNumber,
                })
                .FirstOrDefaultAsync(s => s.TeacherId == id);
			if (teacher == null)
			{
				return NotFound();
			}

			var model = new Teacher
			{
				TeacherId = teacher.TeacherId,
				Name = teacher.Name,
                Address = teacher.Address,
                Email = teacher.Email,      
                PhoneNumber = teacher.PhoneNumber,

			};
			ViewBag.RoleId = new SelectList(_context.Roles, "RoleId", "RoleName", teacher.RoleId);

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditListTE(int id, Teacher model)
		{
			if (id != model.TeacherId)
			{
				return NotFound();
			}
			try
			{
                var teacher = await _context.Teachers // Gọi chay dữ liệu (điều kiện khi 1 trường Null)
                    .Include(s => s.Roles)
                    .Select(e => new Teacher
                    {
                        TeacherId = e.TeacherId,
                        Name = e.Name,
                        Address = e.Address != null ? e.Address : string.Empty,  // Xử lý giá trị null
                        Email = e.Email != null ? e.Email : string.Empty,        // Xử lý giá trị null
                        PhoneNumber = e.PhoneNumber,
                        Username = e.Username,
                        Password = e.Password,
                        RandomKey = e.RandomKey,
                        RoleId = e.RoleId,
                    })
                    .FirstOrDefaultAsync(s => s.TeacherId == id);

                if (teacher == null)
				{
					return NotFound();
				}

				teacher.Name = model.Name;
				teacher.Address = model.Address;
				teacher.Email = model.Email;
				teacher.PhoneNumber = model.PhoneNumber;

				_context.Update(teacher);
				await _context.SaveChangesAsync();
                TempData["ok"] = "Edit Teacher Successfull!";

                return RedirectToAction("ListTeacher", "Admins");
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
			ViewBag.RoleId = new SelectList(_context.Roles, "RoleId", "RoleName", model.RoleId);

			return View(model);
		}
		#endregion

		#region Delete All Info Teacher
		[Authorize(Roles = "Admins")]
		[HttpGet]
        public async Task<IActionResult> DeleteListTE(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .Include(s => s.Roles)
                .FirstOrDefaultAsync(m => m.TeacherId == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        [HttpPost, ActionName("DeleteListTE")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedTE(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                TempData["error"] = "Teacher not found!";
                return RedirectToAction("ListTeacher", "Admins");
            }

            // Set TeacherId to null in related classes
            var classes = await _context.Classes
                .Where(c => c.TeacherId == id)
                .ToListAsync();
            if (classes != null)
            {
                foreach (var classItem in classes)
                {
                    classItem.TeacherId = null;
                }
            }

            // Set TeacherId to null in related schedules
            var schedules = await _context.Schedules
                .Where(s => s.TeacherId == id)
                .ToListAsync();
            if (schedules != null)
            {
                foreach (var schedule in schedules)
                {
                    schedule.TeacherId = null;
                }
            }

            // Set TeacherId to null in related attendances
            var attendances = await _context.Attendance
                .Where(s => s.TeacherId == id)
                .ToListAsync();
            if (attendances != null)
            {
                foreach (var attendance in attendances)
                {
                    attendance.TeacherId = null;
                }
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            TempData["ok"] = "Delete Teacher Successfully!";
            return RedirectToAction("ListTeacher", "Admins");
        }


        #endregion

        #region List student to class
        [Authorize(Roles = "Admins")]
		[HttpGet]
        public async Task<IActionResult> ListStudentToClass()
        {
                    var students = await _context.Students
                .Include(s => s.Major)
                .ThenInclude(m => m.Courses)
                .Where(s => !s.Enrollments.Any()) 
                .Select(s => new StudentViewModel
                {
                    StudentId = s.StudentId,
                    Name = s.Name,
               
                })
        .ToListAsync();
            return View(students);
        }
		#endregion

		#region Add student to class
		[Authorize(Roles = "Admins")]
		[HttpPost]
        public async Task<IActionResult> AddStudentToClass(int studentId, int classId)
        {
            var enrollment = new Enrollment { StudentId = studentId, ClassId = classId };
            _context.Enrollments.Add(enrollment);
            TempData["ok"] = "Add Student To Class Sucessfull!";
            await _context.SaveChangesAsync();

            return RedirectToAction("ListStudentToClass", "Admins");
        }
        [HttpGet]
        public async Task<IActionResult> AddStudentToClass(int studentId)
        {
            var student = await _context.Students
                .Where(s => s.StudentId == studentId)
                .Select(s => new StudentViewModel
                {
                    StudentId = s.StudentId,
                    Name = s.Name
                })
                .FirstOrDefaultAsync();

            if (student == null)
            {
                return NotFound();
            }

            ViewBag.Student = student; // Truyền thông tin sinh viên vào ViewBag
            ViewBag.Classes = await _context.Classes.ToListAsync(); // Truyền danh sách lớp học vào ViewBag

            return View();
        }
        #endregion

        #region Create session
        [HttpGet]
        public async Task<IActionResult> CreateSession()
        {
            var coursesWithoutNumId = await _context.Courses
             .Where(c => c.NumSessions.All(ns => ns.NumId == null))
            .ToListAsync();
            ViewBag.Courses = new SelectList(coursesWithoutNumId, "CourseId", "CourseName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSession(int numses, int courseId)
        {
            if (numses <= 0)
            {
                ModelState.AddModelError(string.Empty, "Numses must be a positive number.");
                return View();
            }

            for (int i = 1; i <= numses; i++)
            {
                var session = new NumSession
                {
                    Numses = i.ToString(),
                    CourseId = courseId
                };

                _context.Num_Session.Add(session);
            }

            await _context.SaveChangesAsync();

            TempData["ok"] = $"{numses} sessions created successfully.";
            return RedirectToAction("Index", "Courses"); 
        }
        #endregion

        #region Logout
        [Authorize(Roles ="Admins")]
        public async Task<IActionResult> Logout()

        {
            await HttpContext.SignOutAsync();
            TempData["ok"] = "See you again!";
            return Redirect("/");
        }
        #endregion

        #region FAQ
        public IActionResult FAQ()
        {
            return View();
        }
        #endregion
    }
}
