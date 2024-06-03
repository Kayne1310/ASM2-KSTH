
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
        
        public IActionResult AdminPage()
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

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("AdminPage", "Admins");
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


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
