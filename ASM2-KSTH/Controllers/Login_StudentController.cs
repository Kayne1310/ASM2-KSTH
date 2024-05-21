using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASM2_KSTH.Data;
using ASM2_KSTH.Models;

namespace ASM2_KSTH.Controllers
{
    public class Login_StudentController : Controller
    {
        private readonly ASM2_KSTHContext _context;

        public Login_StudentController(ASM2_KSTHContext context)
        {
            _context = context;
        }

        // GET: Login_Student
        public async Task<IActionResult> Index()
        {
            return View(await _context.Lstudent.ToListAsync());
        }

        // GET: Login_Student/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login_Student = await _context.Lstudent
                .FirstOrDefaultAsync(m => m.Id == id);
            if (login_Student == null)
            {
                return NotFound();
            }

            return View(login_Student);
        }
    }
}
        
