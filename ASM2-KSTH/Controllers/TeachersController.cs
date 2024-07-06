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
using ASM2_KSTH.Helpers;
using ASM2_KSTH.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASM2_KSTH.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ASM2_KSTHContext _context;

        public TeachersController(ASM2_KSTHContext context)
        {
            _context = context;
        }

        #region Login for Teacher
        // GET: Teachers
        [HttpGet]
        public IActionResult Index(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            ViewData["ReturnUrl"] = ReturnUrl;
            return View();
        }


        // POST: Teachers
        [HttpPost]
        public async Task<IActionResult> Index(Teacher model, string? ReturnUrl)
        {

            ViewBag.ReturnUrl = ReturnUrl;

    
                // Thực hiện xác thực thông tin đăng nhập tại đây


                // Thực hiện xác thực thông tin đăng nhập tại đây
                var teacher = _context.Teachers.SingleOrDefault(u => u.Username == model.Username);

                if (teacher == null || teacher.Password != model.Password.ToMd5Hash(teacher.RandomKey))
                {
                    // Xác thực thất bại, đặt thông báo lỗi vào ViewBag và hiển thị lại form đăng nhập
                    ViewBag.ErrorMessage = "Invalid username or password.";
                     TempData["no"] = "Invalid username or password.";
              
                    return View("Index", model);

                }
                else
                {
                    var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == teacher.RoleId);
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, teacher.TeacherId.ToString()),
                        new Claim(ClaimTypes.Name, teacher.Username),
                        new Claim(ClaimTypes.Role, "Teachers"),
                        new Claim("FullName", teacher.Name),
                        new Claim("Email", teacher.Email ?? string.Empty),
                    };
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

                        return RedirectToAction("DashBoard", "Teachers");

                    }

                }
                return View();

        }
		#endregion

		#region Change password for Teacher
		[HttpGet]
		[Authorize(Roles = "Teachers")]
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

			var teacher = await _context.Teachers.FindAsync(int.Parse(userId));
			if (teacher == null)
			{
				return NotFound();
			}

			// Validate old password
			if (teacher.Password != oldPassword.ToMd5Hash(teacher.RandomKey))
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
			teacher.Password = newPassword.ToMd5Hash(teacher.RandomKey);
			_context.Update(teacher);
			await _context.SaveChangesAsync();

			TempData["ok"] = "Password changed successfully!";
			return RedirectToAction("DashBoard", "Teachers");
		}

		#endregion
		public async Task<IActionResult> DashBoard()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var teacherId = int.Parse(userId);
            var teachername = await _context.Teachers
                                .Where(a => a.TeacherId == teacherId)
                                .Select(a => a.Name)
                                .FirstOrDefaultAsync();
            ViewBag.TeacherName = teachername;
            return View();
        }

        #region Profile for Teacher

        [Authorize(Roles = "Teachers")]
        [HttpGet]
        public async Task<IActionResult> ProfileTE()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("Index", "Teachers");
            }

            var teacherId = int.Parse(userId);
            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(s => s.TeacherId == teacherId);

            if (teacher == null)
            {
                return NotFound();
            }

            var model = new Teacher
            {
                TeacherId = teacher.TeacherId,
                Name = teacher.Name,
                Address = teacher.Address ?? string.Empty,
                PhoneNumber = teacher.PhoneNumber,
                Email = teacher.Email ?? string.Empty,
                Username = teacher.Username,
            };
            ViewBag.TeacherName = teacher.Name;
            ViewBag.TeacherEmail = teacher.Email;

            ViewBag.RoleId = new SelectList(_context.Roles, "RoleId", "RoleName", teacher.RoleId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProfileTE(Teacher model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // lấy dữ liệu thông tin khi đăng nhập thành công
            if (userId == null)
            {
                return RedirectToAction("Index", "Teachers");
            }

            var teacherId = int.Parse(userId);
            if (teacherId != model.TeacherId)
            {
                return NotFound();
            }
            try
            {
                // Gọi chay dữ liệu (điều kiện khi một trường có giá trị null)
                var teacher = await _context.Teachers
                    .FirstOrDefaultAsync(s => s.TeacherId == teacherId);

                if (teacher == null)
                {
                    return NotFound();
                }

                teacher.Name = model.Name;
                teacher.Address = model.Address ?? string.Empty; // Xử lý giá trị null
                teacher.PhoneNumber = model.PhoneNumber;
                teacher.Email = model.Email ?? string.Empty;     // Xử lý giá trị null

                _context.Update(teacher);
                await _context.SaveChangesAsync();
                TempData["ok"] = "Edit Profile Teacher Sucessful";
                ViewBag.TeacherName = teacher.Name;
                ViewBag.TeacherEmail = teacher.Email;

                return RedirectToAction("DashBoard", "Teachers"); //// 
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

        [Authorize(Roles = "Teachers")]
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync();
            TempData["ok"] = "See you again !";
            return Redirect("/");
        }

      

    }
}
