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
                        new Claim(ClaimTypes.Role, "Students"),
                        new Claim(MySetting.CLAIM_ID, student.Username),
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
                        return RedirectToAction("Index", "Home");
                    }
                }

            return View();
        }
        #endregion


        [Authorize]
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
