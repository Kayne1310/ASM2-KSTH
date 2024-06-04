using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASM2_KSTH.Data;
using ASM2_KSTH.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ASM2_KSTH.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;


namespace ASM2_KSTH.Controllers.Grade
{
    public class GradesController : Controller
    {
        private readonly ASM2_KSTHContext _context;

        public GradesController(ASM2_KSTHContext context)
        {
            _context = context;
        }

      
        [Authorize(Roles = "Teachers")]
        public IActionResult Index()
        {
            var courses = _context.Courses.ToList();
            ViewData["Courses"] = new SelectList(courses, "CourseId", "CourseName");

            int defaultCourseId = courses.FirstOrDefault()?.CourseId ?? 0;
            return View();
        }

        [HttpPost]
        public IActionResult Index(int courseId)
        {
            var courses = _context.Courses.ToList();
            ViewData["Courses"] = new SelectList(courses, "CourseId", "CourseName");

            var classes = _context.Classes.Where(c => c.CourseId == courseId).ToList();
            ViewData["Classes"] = new SelectList(classes, "ClassId", "ClassName");

            return View("Index");
        }


        [Authorize(Roles = "Teachers")]
        [HttpGet]
        public async Task<IActionResult> ListStudents(int classId)
        {
            var students = await _context.Enrollments
                .Include(e => e.Student)
                .ThenInclude(s => s.Major)
                .ThenInclude(c => c.Courses)
                .ThenInclude(e => e.Grades)
                .Where(e => e.ClassId == classId)
                .Select(e => new StudentViewModel
                {
                    StudentId = e.Student.StudentId,
                    Name = e.Student.Name,
                    ClassName = e.Class.ClassName,
                    MajorName = e.Student.Major.MajorName,
                    Grade1 = e.Grades.FirstOrDefault() != null ? e.Grades.FirstOrDefault().Grade1 : null
                })
                .ToListAsync();

            ViewData["ClassId"] = classId;
            return View(students);
        }

        [HttpPost]
        public async Task<IActionResult> ListStudents(int classId, string actionType)
        {
            var students = await _context.Enrollments
                .Include(e => e.Student)
                .ThenInclude(s => s.Major)
                .ThenInclude(c => c.Courses)
                .ThenInclude(e => e.Grades)
                .Where(e => e.ClassId == classId)
                .Select(e => new StudentViewModel
                {
                    StudentId = e.Student.StudentId,
                    Name = e.Student.Name,
                    ClassName = e.Class.ClassName,
                    MajorName = e.Student.Major.MajorName,
                    Grade1 = e.Grades.FirstOrDefault() != null ? e.Grades.FirstOrDefault().Grade1 : null
                })
                .ToListAsync();
            ViewData["ClassId"] = classId;
            return View(students);
        }

        [Authorize(Roles = "Teachers")]
        public async Task<IActionResult> Edit(int id, int classId)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }

            var model = new GradesViewModels
            {
                GradeId = grade.GradeId,
                Grade1 = grade.Grade1,
                Students = _context.Students.ToList(),
                Courses = _context.Courses.ToList(),
                Classes = _context.Classes.ToList()
            };

            
            ViewData["ClassId"] = classId;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GradesViewModels model, int classId)
        {
            if (id != model.GradeId)
            {
                return NotFound();
            }

            
                try
                {
                    var grade = await _context.Grades.FindAsync(id);
                    if (grade == null)
                    {
                        return NotFound();
                    }

                    grade.Grade1 = model.Grade1;
                    _context.Update(grade);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("ListStudents", new { classId = classId });
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Unable to save changes. The grade was updated or deleted by another user.");
                }
             

            model.Students = _context.Students.ToList();
            model.Courses = _context.Courses.ToList();
            model.Classes = _context.Classes.ToList();
            
            ViewData["ClassId"] = classId;
            return View(model);
        }

        [Authorize(Roles = "Teachers")]
        public async Task<IActionResult> Delete(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade != null)
            {
                _context.Grades.Remove(grade);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }

}
