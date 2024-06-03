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

namespace ASM2_KSTH.Controllers.Grade
{
    public class GradesController : Controller
    {
        private readonly ASM2_KSTHContext _context;

        public GradesController(ASM2_KSTHContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var courses = _context.Courses.ToList();
            ViewData["Courses"] = new SelectList(courses, "CourseId", "CourseName");

            // Mặc định chọn khóa học đầu tiên nếu có
            int defaultCourseId = courses.FirstOrDefault()?.CourseId ?? 0;
            return View();
        }

        [HttpPost]
        public IActionResult Index(int courseId)
        {


            // Lấy danh sách các môn học
            var courses = _context.Courses.ToList();
            ViewData["Courses"] = new SelectList(courses, "CourseId", "CourseName");

            var classes = _context.Classes.Where(c => c.CourseId == courseId).ToList();
            ViewData["Classes"] = new SelectList(classes, "ClassId", "ClassName");

            return View("Index");
        }


        [HttpPost]
        public async Task<IActionResult> ListStudents(int classId)
        {
            var students = await _context.Enrollments
                .Include(e => e.Student)
                .ThenInclude(s => s.Major)
                .Where(e => e.ClassId == classId)
                .Select(e => new StudentViewModel
                {
                    StudentId = e.Student.StudentId,
                    Name = e.Student.Name,
                   
                    Address = e.Student.Address,
                    PhoneNumber = e.Student.PhoneNumber,
                    Email = e.Student.Email,
                    Username = e.Student.Username,
                    MajorName = e.Student.Major.MajorName
                })
                .ToListAsync();

            return View(students);
        }




    }
}
