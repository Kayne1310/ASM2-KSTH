﻿using System;
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

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("StudentPage", "Students");
                    }
                }

            return View();
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

                ViewBag.StudentName = student.Name;
                ViewBag.StudentEmail = student.Email;

                return RedirectToAction("StudentPage", "Students");
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
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        [Authorize(Roles = "Students")]
        public ActionResult StudentPage()
        {
            return View();
        }
    }
}
