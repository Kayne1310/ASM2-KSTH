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
                var teacher = _context.Lteacher.SingleOrDefault(u => u.Username == model.Username);

                if (teacher == null || teacher.Password != model.Password.ToMd5Hash(teacher.RandomKey))
                {
                    // Xác thực thất bại, đặt thông báo lỗi vào ViewBag và hiển thị lại form đăng nhập
                    ViewBag.ErrorMessage = "Invalid username or password.";
                    return View("Index", model);

                }
                else
                {
                    var claims = new List<Claim>
                  {
                      new Claim(MySetting.CLAIM_ID, teacher.Username),
                  };
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

        }
    #endregion



        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
