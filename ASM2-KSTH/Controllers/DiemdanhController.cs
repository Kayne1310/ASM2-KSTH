using ASM2_KSTH.Data;
using ASM2_KSTH.Models;
using ASM2_KSTH.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace ASM2_KSTH.Controllers
{
    public class DiemdanhController : Controller
    {


        private readonly ASM2_KSTHContext _context;

        public DiemdanhController(ASM2_KSTHContext context)
        {
            _context = context;
        }

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


    
        [HttpGet]
        public async Task<IActionResult> Diemdanh(int classId)
        {
            var students = await _context.Enrollments
               .Include(e => e.Attendance)
               .Include(e => e.Student)
                   .ThenInclude(s => s.Major)
               .Include(e => e.Class)
                   .ThenInclude(c => c.Course)  // Include Course from Class
                       .ThenInclude(c => c.NumSessions)  // Include NumSessions from Course
               .Include(e => e.Grades)
               .Where(e => e.ClassId == classId)
               .Select(e => new StudentViewModel
               {
                   StudentId = e.Student.StudentId,
                   Name = e.Student.Name,
                   ClassName = e.Class.ClassName,
                   MajorName = e.Student.Major.MajorName,
                   EnrollmentId = e.Id,
                   AttendanceStatus = e.Attendance.FirstOrDefault() != null ? e.Attendance.FirstOrDefault().AttendanceStatus : null,
                   Reason = e.Attendance.FirstOrDefault() != null ? e.Attendance.FirstOrDefault().Reason : null,
                   AttendanceDate = e.Attendance.FirstOrDefault() != null ? (DateTime?)e.Attendance.FirstOrDefault().AttendanceDate.ToDateTime(new TimeOnly(0, 0)) : null,
                   Numses = e.Class.Course.NumSessions.Select(ns => new NumSessionViewModel { NumId = ns.NumId, Numses = ns.Numses }).ToList()
               })
               .ToListAsync();

            ViewData["ClassId"] = classId;
            return View(students);
        }


        [HttpPost]
        public async Task<IActionResult> Diemdanh(int classId, List<StudentViewModel> postedStudents)
        {
            var students = await _context.Enrollments
                .Include(e => e.Attendance)
                .Include(e => e.Student)
                    .ThenInclude(s => s.Major)
                .Include(e => e.Class)
                    .ThenInclude(c => c.Course)  // Include Course from Class
                .Include(e => e.Grades)
                .Include(e => e.Class)
                    .ThenInclude(c => c.Teacher)
                .Include(e => e.Class)
                    .ThenInclude(c => c.Room)
                .Where(e => e.ClassId == classId)
                
                .Select(e => new StudentViewModel
                {
                    StudentId = e.Student.StudentId,
                    Name = e.Student.Name,
                    ClassName = e.Class.ClassName,
                    MajorName = e.Student.Major.MajorName,
                    EnrollmentId = e.Id,
                    AttendanceStatus = e.Attendance.FirstOrDefault() != null ? e.Attendance.FirstOrDefault().AttendanceStatus : null,
                    Reason = e.Attendance.FirstOrDefault() != null ? e.Attendance.FirstOrDefault().Reason : null,
                    AttendanceDate = e.Attendance.FirstOrDefault() != null ? (DateTime?)e.Attendance.FirstOrDefault().AttendanceDate.ToDateTime(new TimeOnly(0, 0)) : null,
                    Numses = e.Class.Course.NumSessions.Select(ns => new NumSessionViewModel { NumId = ns.NumId, Numses = ns.Numses }).ToList(),
                    TeacherId = e.Class.TeacherId,
                    RoomId = e.Class.Room.RoomId
                })
                .ToListAsync();

            ViewData["ClassId"] = classId;

            return View(students);
        }


        [HttpPost]
        public async Task<IActionResult> Save()
        {
            var form = Request.Form;

            if (form == null || !form.Keys.Any())
            {
                return BadRequest("No data received for attendance.");
            }

            try
            {
                foreach (var key in form.Keys)
                {
                    if (key.StartsWith("students[") && key.EndsWith("].AttendanceStatus"))
                    {
                        var enrollmentId = Convert.ToInt32(key.Split("[")[1].Split("]")[0]);
                        var attendanceStatus = form[key];
                        var reason = form[$"students[{enrollmentId}].Reason"];
                        var numId = Convert.ToInt32(form[$"students[{enrollmentId}].NumId"]);
                        var attendanceDate = DateTime.Now; // Use the current date or get it from the form if available
                        var teacherId = Convert.ToInt32(form[$"students[{enrollmentId}].TeacherId"]);
                        var roomId = Convert.ToInt32(form[$"students[{enrollmentId}].RoomId"]);

                        var enrollment = await _context.Enrollments
                            .FirstOrDefaultAsync(e => e.Id == enrollmentId);

                        if(numId == 0)
                        {
                            TempData["no"] = $"Failed to save attendance ";
                            return RedirectToAction("Index", "Diemdanh");
                        }
                        if (enrollment != null)
                        {
                            var studentId = enrollment.StudentId;
                            var classId = enrollment.ClassId;

                            // Check if attendance exists for the student
                            var existingAttendance = await _context.Attendance
                                .FirstOrDefaultAsync(a => a.StudentId == studentId && a.ClassId == classId && a.NumId==numId);

                            if (existingAttendance != null)
                            {
                                // Update existing attendance
                                existingAttendance.AttendanceStatus = attendanceStatus;
                                existingAttendance.Reason = reason;
                                existingAttendance.AttendanceDate = DateOnly.FromDateTime(attendanceDate);
                                existingAttendance.NumId = numId;
                                existingAttendance.TeacherId = teacherId;
                                existingAttendance.RoomId = roomId;

                                // Update the entity in the context (no need to Add)
                                _context.Attendance.Update(existingAttendance);
                            }
                            else
                            {
                                // Create new attendance
                                var newAttendance = new Models.Attendance
                                {
                                    StudentId = studentId,
                                    ClassId = classId,
                                    EnrollmentId = enrollment.Id,
                                    AttendanceStatus = attendanceStatus,
                                    Reason = reason,
                                    AttendanceDate = DateOnly.FromDateTime(attendanceDate),
                                    NumId = numId,
                                    TeacherId = teacherId,
                                    RoomId = roomId
                                };

                                // Add the new attendance to the context
                                _context.Attendance.Add(newAttendance);
                            }
                        }
                    }
                }

                await _context.SaveChangesAsync();
                TempData["ok"] = "Attendance saved successfully.";
                return RedirectToAction("Index", "Diemdanh");
            }
            catch (Exception ex)
            {
                TempData["no"] = $"Failed to save attendance: {ex.Message}";
                return RedirectToAction("Index", "Diemdanh");
            }
        }


    }
}
