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
    public class SchedulesController : Controller
    {
        private readonly ASM2_KSTHContext _context;

        public SchedulesController(ASM2_KSTHContext context)
        {
            _context = context;
        }

        // GET: Schedules
        public async Task<IActionResult> Index()
        {
            var aSM2_KSTHContext = _context.Schedule_1.Include(s => s.Class).Include(s => s.Course).Include(s => s.Room);
            return View(await aSM2_KSTHContext.ToListAsync());
        }

        // GET: Schedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule_1
                .Include(s => s.Class)
                .Include(s => s.Course)
                .Include(s => s.Room)
                .FirstOrDefaultAsync(m => m.ScheduleID == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET: Schedules/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId");
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId");
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScheduleID,ClassId,CourseId,RoomId,Day,StartTime,EndTime")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", schedule.ClassId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", schedule.CourseId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId", schedule.RoomId);
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule_1.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", schedule.ClassId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", schedule.CourseId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId", schedule.RoomId);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScheduleID,ClassId,CourseId,RoomId,Day,StartTime,EndTime")] Schedule schedule)
        {
            if (id != schedule.ScheduleID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.ScheduleID))
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
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", schedule.ClassId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", schedule.CourseId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId", schedule.RoomId);
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule_1
                .Include(s => s.Class)
                .Include(s => s.Course)
                .Include(s => s.Room)
                .FirstOrDefaultAsync(m => m.ScheduleID == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedule = await _context.Schedule_1.FindAsync(id);
            if (schedule != null)
            {
                _context.Schedule_1.Remove(schedule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedule_1.Any(e => e.ScheduleID == id);
        }
    }
}
