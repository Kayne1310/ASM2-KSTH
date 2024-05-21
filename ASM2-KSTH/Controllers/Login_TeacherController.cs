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
    public class Login_TeacherController : Controller
    {
        private readonly ASM2_KSTHContext _context;

        public Login_TeacherController(ASM2_KSTHContext context)
        {
            _context = context;
        }

        // GET: Login_Teacher
        public async Task<IActionResult> Index()
        {
            return View(await _context.Lteacher.ToListAsync());
        }

        // GET: Login_Teacher/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login_Teacher = await _context.Lteacher
                .FirstOrDefaultAsync(m => m.Id == id);
            if (login_Teacher == null)
            {
                return NotFound();
            }

            return View(login_Teacher);
        }

        // GET: Login_Teacher/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Login_Teacher/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Username,Password")] Login_Teacher login_Teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(login_Teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(login_Teacher);
        }

        // GET: Login_Teacher/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login_Teacher = await _context.Lteacher.FindAsync(id);
            if (login_Teacher == null)
            {
                return NotFound();
            }
            return View(login_Teacher);
        }

        // POST: Login_Teacher/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Username,Password")] Login_Teacher login_Teacher)
        {
            if (id != login_Teacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(login_Teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Login_TeacherExists(login_Teacher.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(login_Teacher);
        }

        // GET: Login_Teacher/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login_Teacher = await _context.Lteacher
                .FirstOrDefaultAsync(m => m.Id == id);
            if (login_Teacher == null)
            {
                return NotFound();
            }

            return View(login_Teacher);
        }

        // POST: Login_Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var login_Teacher = await _context.Lteacher.FindAsync(id);
            if (login_Teacher != null)
            {
                _context.Lteacher.Remove(login_Teacher);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Login_TeacherExists(int id)
        {
            return _context.Lteacher.Any(e => e.Id == id);
        }
    }
}
