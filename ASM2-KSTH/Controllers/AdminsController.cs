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

namespace ASM2_KSTH.Controllers
{
    public class AdminsController : Controller
    {
        private readonly ASM2_KSTHContext _context;

        public AdminsController(ASM2_KSTHContext context)
        {
            _context = context;
        }

        // GET: Admins
        [HttpGet]
        public IActionResult Index(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        // POST: Admins
        [HttpPost]
        public async Task<IActionResult> Index(Admin model, string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            if (ModelState.IsValid)
            {
                // Thực hiện xác thực thông tin đăng nhập tại đây
                var user = await _context.Ladmin.FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);

                if (user != null)
                {
                    // Xác thực thành công, chuyển hướng đến trang mong muốn
                    if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    // Xác thực thất bại, đặt thông báo lỗi vào ViewBag và hiển thị lại form đăng nhập
                    ViewBag.ErrorMessage = "Invalid username or password.";
                    return View("Index", model);
                }
            }
            return View("Index", model);
        }







        [HttpGet]
        public async Task<IActionResult> AddStudent()
        {
            var students = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Class)
                .Where(e => e.Student.Major.Courses.Any()) // Chỉ lấy sinh viên đã được ghi danh vào ít nhất một khóa học
                .Select(e => new StudentViewModel
                {
                    StudentId = e.Student.StudentId,
                    Name = e.Student.Name,
                    ClassName = e.Class.ClassName,
                    CourseName = e.Class != null ? e.Class.Course.CourseName : null
                })
                .ToListAsync();

            return View(students);
        }


        [HttpPost]
        public async Task<IActionResult> Add(int studentId, int classId)
        {
            var enrollment = new Enrollment { StudentId = studentId, ClassId = classId };
            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
            return RedirectToAction("AddStudent", "Admins", new { classId = classId });
        }

        [HttpGet]
        public async Task<IActionResult> Add(int studentId)
        {
            var students = await _context.Students
                .Select(s => new StudentViewModel
                {
                    StudentId = s.StudentId,
                    Name = s.Name
                })
                .ToListAsync();

            ViewBag.Classes = await _context.Classes.ToListAsync(); // Gán giá trị cho ViewBag.Classes

            return View(students);
        }


        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
