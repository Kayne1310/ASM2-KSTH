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
                    Grade1 = e.Grades.FirstOrDefault() != null ? e.Grades.FirstOrDefault().Grade1 : null,
                    EnrollmentId = e.Id,
                    GradeId = e.Grades.FirstOrDefault() != null ? e.Grades.FirstOrDefault().GradeId : (int?)null
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
                    Grade1 = e.Grades.FirstOrDefault() != null ? e.Grades.FirstOrDefault().Grade1 : null,
                    CourseId = e.Class.CourseId.HasValue ? e.Class.CourseId.Value : 0,
                    EnrollmentId = e.Id,
                    GradeId = e.Grades.FirstOrDefault() != null ? e.Grades.FirstOrDefault().GradeId : (int?)null 
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
        public async Task<IActionResult> Add(int id, int classId, int? studentId, int courseId, int enrollmentId)
        {
            var student = await _context.Students.FindAsync(id);
            var classes = _context.Classes.ToList();

            // Nếu không tìm thấy sinh viên, tạo một đối tượng Grade mới để thêm điểm
     

          

            // Nếu tìm thấy sinh viên, trả về View để sửa điểm
            var editModel = new GradesViewModels
            {
                SelectedStudentId =id,
                StudentName = student.Name,
                Classes = classes,
                SelectedCourseId = courseId,
                SelectedEnrollmentId = enrollmentId
            };

            ViewData["ClassId"] = classId;
            return View(editModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int id, GradesViewModels model, int classId, int? studentId, int courseId, int enrollmentId)
        {
            try
            {
                // Nếu tìm thấy học sinh, thêm điểm cho học sinh đó
                var newGrade = new ASM2_KSTH.Models.Grade
                {
                    Grade1 = model.Grade1,
                    CourseId = courseId,
                    EnrollmentId = enrollmentId
                };

                _context.Add(newGrade);
                await _context.SaveChangesAsync();
                return RedirectToAction("ListStudents", new { classId = classId });
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "Unable to save changes. The grade was updated or deleted by another user.");
            }

            // Nếu có lỗi xảy ra, trả về View với model và dữ liệu cần thiết
            model.Students = _context.Students.ToList();
            model.Classes = _context.Classes.ToList();
            ViewData["ClassId"] = classId;
            return View(model);
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
