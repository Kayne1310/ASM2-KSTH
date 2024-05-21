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
    public class Login_AdminController : Controller
    {
        private readonly ASM2_KSTHContext _context;

        public Login_AdminController(ASM2_KSTHContext context)
        {
            _context = context;
        }

        // GET: Login_Admin
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ladmin.ToListAsync());
        }

        // GET: Login_Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login_Admin = await _context.Ladmin
                .FirstOrDefaultAsync(m => m.Id == id);
            if (login_Admin == null)
            {
                return NotFound();
            }

            return View(login_Admin);
        }

        // GET: Login_Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Login_Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Username,Password")] Login_Admin login_Admin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(login_Admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(login_Admin);
        }

        // GET: Login_Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login_Admin = await _context.Ladmin.FindAsync(id);
            if (login_Admin == null)
            {
                return NotFound();
            }
            return View(login_Admin);
        }

        // POST: Login_Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Username,Password")] Login_Admin login_Admin)
        {
            if (id != login_Admin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(login_Admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Login_AdminExists(login_Admin.Id))
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
            return View(login_Admin);
        }

        // GET: Login_Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login_Admin = await _context.Ladmin
                .FirstOrDefaultAsync(m => m.Id == id);
            if (login_Admin == null)
            {
                return NotFound();
            }

            return View(login_Admin);
        }

        // POST: Login_Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var login_Admin = await _context.Ladmin.FindAsync(id);
            if (login_Admin != null)
            {
                _context.Ladmin.Remove(login_Admin);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Login_AdminExists(int id)
        {
            return _context.Ladmin.Any(e => e.Id == id);
        }
    }
}
