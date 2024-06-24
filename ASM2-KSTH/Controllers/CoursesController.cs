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
    public class CoursesController : Controller
    {
        private readonly ASM2_KSTHContext _context;

        public CoursesController(ASM2_KSTHContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses
                                        .Include(c => c.Major)
                                        .Include(c => c.Classes)
                                        .ToListAsync();
            return View(courses);
        }



        // GET: Courses/Create
        public IActionResult Create()
        {
            ViewData["MajorId"] = new SelectList(_context.Majors, "MajorId", "MajorId");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,CourseName,CourseDescription,Credits,MajorId")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MajorId"] = new SelectList(_context.Majors, "MajorId", "MajorId", course.MajorId);
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["MajorId"] = new SelectList(_context.Majors, "MajorId", "MajorId", course.MajorId);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,CourseName,CourseDescription,Credits,MajorId")] Course course)
        {
            if (id != course.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.CourseId))
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
            ViewData["MajorId"] = new SelectList(_context.Majors, "MajorId", "MajorId", course.MajorId);
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Major)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [HttpGet]
        public async Task<IActionResult> Add(int id)
        {
            var selectedCourse = await _context.Courses.Include(c => c.Major).FirstOrDefaultAsync(c => c.CourseId == id);
            if (selectedCourse == null)
            {
                return NotFound();
            }

            var classes = await _context.Classes.ToListAsync();
            ViewData["SelectedCourse"] = selectedCourse;
            ViewData["Classes"] = new SelectList(classes, "ClassId", "ClassName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int classId, int courseId)
        {

            var selectedClass = await _context.Classes.FindAsync(classId);
            if (selectedClass != null)
            {
                // Tạo một bản ghi mới trong trường hợp này
                var newClass = new Class
                {
                    // Copy các thông tin từ lớp học được chọn
                    CourseId = courseId,
                    ClassName = selectedClass.ClassName,
                    RoomId= selectedClass.RoomId,
                    Year= selectedClass.Year,
                    Semester= selectedClass.Semester,
                    TeacherId= selectedClass.TeacherId,

                  
                };
               
                _context.Add(newClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Unable to update class with selected course.");
            var selectedCourse = await _context.Courses.FindAsync(courseId);
            var classes = await _context.Classes.ToListAsync();
            ViewData["SelectedCourse"] = selectedCourse;
            ViewData["Classes"] = new SelectList(classes, "ClassId", "ClassName");

            return View();
        }


        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.CourseId == id);
        }
    }
}
