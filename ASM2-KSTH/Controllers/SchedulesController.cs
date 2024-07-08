using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASM2_KSTH.Data;
using ASM2_KSTH.Models;
using ASM2_KSTH.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ASM2_KSTH.Controllers
{
    public class SchedulesController : Controller
    {
        private readonly ASM2_KSTHContext _context;

        public SchedulesController(ASM2_KSTHContext context)
        {
            _context = context;
        }

		#region List schedule
		[HttpGet]
		public async Task<IActionResult> Index(int EnrollmentID, int CourseID, int RoomID, int SlotID, int TeacherID)
		{
			var schedules = await _context.Schedules
				.Include(s => s.Enrollments)
				.ThenInclude(e => e.Class)  
				.Include(s => s.Courses)
				.Include(s => s.Rooms)
				.Include(s => s.Slots)
				.Include(s => s.Teachers)
				.Select(e => new ScheduleVM
				{
					ScheduleId = e.ScheduleId,
					EnrollmentId = e.EnrollmentId,
                    Day = e.Day,
                    CourseName = e.Courses.CourseName,
					RoomNumber = e.Rooms.RoomNumber,
					ClassName = e.Enrollments.Class.ClassName,
                    SlotName = e.Slots.SlotName,
                    Name = e.Teachers.Name,
				})
				.ToListAsync();
				ViewData["EnrollmentID"] = EnrollmentID;
				ViewData["CourseID"] = CourseID;
				ViewData["RoomID"] = RoomID;
				ViewData["SlotID"] = SlotID;
				ViewData["TeacherID"] = TeacherID;
				return View(schedules);
		}

		[HttpPost]
		public async Task<IActionResult> Index(int EnrollmentID, int CourseID, int RoomID, int SlotID, int TeacherID, string actionType)
		{
			 //Kiểm tra actionType để xác định hành động cần thực hiện
			switch (actionType)
			{
				case "list":
					var schedules = await _context.Schedules
					.Include(s => s.Enrollments)
					.ThenInclude(e => e.Class)   //Bao gồm thực thể Class thông qua Enrollment
					.Include(s => s.Courses)
					.Include(s => s.Rooms)
                    .Include(s => s.Slots)
                    .Include(s => s.Teachers)
                    .Select(e => new ScheduleVM
					{
                        ScheduleId = e.ScheduleId,
                        EnrollmentId = e.EnrollmentId,
                        Day = e.Day,
                        CourseName = e.Courses.CourseName,
                        RoomNumber = e.Rooms.RoomNumber,
                        ClassName = e.Enrollments.Class.ClassName,
                        SlotName = e.Slots.SlotName,
                        Name = e.Teachers.Name,
                    })
					.ToListAsync();
					return View("Index", schedules);

					 //Thêm các trường hợp khác tương ứng với các actionType khác nhau

				default:
					return BadRequest("Invalid action type");  //return bad request if actionType is not expected
			}
		}
        #endregion

        #region Edit schedule
        [HttpGet]
        public async Task<IActionResult> EditSchedule(int id)
        {
            var schedules = await _context.Schedules
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Class)
                .Include(s => s.Courses)
                .Include(s => s.Rooms)
                .Include(s => s.Slots)
                .Include(s => s.Teachers)
               .Select(e => new ScheduleVM
               {
                   ScheduleId = e.ScheduleId,
                   Day = e.Day,
                   SlotName = e.Slots.SlotName,
                   EnrollmentId = e.EnrollmentId,
                   CourseId = e.CourseId,
                   CourseName = e.Courses.CourseName,
                   RoomId = e.RoomId,
                   RoomNumber = e.Rooms.RoomNumber,
                   ClassId = e.Enrollments.Class.ClassId,
                   ClassName = e.Enrollments.Class.ClassName,
                   TeacherId = e.TeacherId,
                   Name = e.Teachers.Name,
               })
                .FirstOrDefaultAsync(e => e.ScheduleId == id);

            if (schedules == null)
            {
                return NotFound();
            }

            var model = new ScheduleVM
            {
                ScheduleId = schedules.ScheduleId,
                Day = schedules.Day,
                EnrollmentId = schedules.EnrollmentId,
                SlotId = schedules.SlotId,
                SlotName = schedules.SlotName,
                CourseId = schedules.CourseId,
                CourseName = schedules.CourseName,
                RoomId = schedules.RoomId,
                RoomNumber = schedules.RoomNumber,
                ClassId = schedules.ClassId,
                ClassName = schedules.ClassName,
                TeacherId = schedules.TeacherId,
                Name = schedules.Name,
            };

            ViewBag.ClassName = new SelectList(await _context.Classes
                   .Select(c => new { c.ClassId, c.ClassName })
                   .ToListAsync(), "ClassId", "ClassName", model.ClassId);
            ViewBag.RoomNumber = new SelectList(_context.Rooms, "RoomId", "RoomNumber", model.RoomId);
            ViewBag.CourseName = new SelectList(_context.Courses, "CourseId", "CourseName", model.CourseId);
            ViewBag.SlotName = new SelectList(_context.Slots, "SlotId", "SlotName", model.SlotId);
            ViewBag.Name = new SelectList(_context.Teachers, "TeacherId", "Name", model.TeacherId);

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSchedule(int id, ScheduleVM model)
            {
            if (id != model.ScheduleId)
            {
                return NotFound();
            }
            try
            {
                var schedule = await _context.Schedules
                .FirstOrDefaultAsync(e => e.ScheduleId == id);

                var enrollment = await _context.Enrollments
                    .Where(e => e.ClassId == model.ClassId)
                    .Select(e => e.EnrollmentId)
                    .FirstOrDefaultAsync();

                if (schedule == null)
                {
                    return NotFound();
                }
                schedule.Day = model.Day;
                schedule.EnrollmentId = enrollment;
                schedule.SlotId = model.SlotId;  //????
                schedule.CourseId = model.CourseId;
                schedule.RoomId = model.RoomId;
                schedule.TeacherId = model.TeacherId;

                 //Tìm ClassId dựa trên ClassName
                var classes = await _context.Enrollments.FirstOrDefaultAsync(m => m.ClassId == model.ClassId);
                if (classes == null)
                {
                    ModelState.AddModelError("", "Invalid major selected.");
                    ViewBag.ClassId = new SelectList(_context.Classes, "ClassId", "ClassId", model.ClassId);
                    return View(model);
                }
                _context.Update(schedule);
                await _context.SaveChangesAsync();
                TempData["ok"] = "Edit successful!";
                return RedirectToAction("Index", "Schedules");
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "Unable to save changes. The student was updated or deleted by another user.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while saving changes: {ex.Message}");
            }
             //Nếu có lỗi xảy ra, cần điền lại dữ liệu cho model để hiển thị lại view
            ViewBag.ClassName = new SelectList(await _context.Classes
               .Select(c => new { c.ClassId, c.ClassName })
               .ToListAsync(), "ClassId", "ClassName", model.ClassId);
            ViewBag.RoomNumber = new SelectList(_context.Rooms, "RoomId", "RoomNumber", model.RoomId);
            ViewBag.CourseName = new SelectList(_context.Courses, "CourseId", "CourseName", model.CourseId);
            ViewBag.SlotName = new SelectList(_context.Slots, "SlotId", "SlotName", model.SlotId);
            ViewBag.Name = new SelectList(_context.Slots, "TeacherId", "Name", model.TeacherId);

            return View(model);
            }
        #endregion

        #region Delete schedule
        [HttpGet]
        public async Task<IActionResult> DeleteSchedule(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Class)
                .Include(s => s.Courses)
                .Include(s => s.Rooms)
                .Include(s => s.Slots)
                .Include(s => s.Teachers)
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        [HttpPost, ActionName("DeleteSchedule")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedST(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();
            TempData["ok"] = "Delete successful!";
            return RedirectToAction("Index", "SChedules");
        }
        #endregion

        #region Create schedule

        [HttpGet]
        public async Task <IActionResult> CreateSchedule()
        {
            ViewBag.ClassName = new SelectList(await _context.Classes
                   .Select(c => new { c.ClassId, c.ClassName })
                   .ToListAsync(), "ClassId", "ClassName");
            ViewBag.RoomNumber = new SelectList(_context.Rooms, "RoomId", "RoomNumber");
            ViewBag.CourseName = new SelectList(_context.Courses, "CourseId", "CourseName");
            ViewBag.SlotName = new SelectList(_context.Slots, "SlotId", "SlotName");
            ViewBag.Name = new SelectList(_context.Teachers, "TeacherId", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSchedule(ScheduleVM model)
        {
            try
            {
                if (model == null)
                {
                    return NotFound();
                }

                var enrollments = await _context.Enrollments
                    .Where(e => e.ClassId == model.ClassId)
                    .ToListAsync(); // tao mang co id tuong ung 

                if (!enrollments.Any())
                {
                    ModelState.AddModelError("", "No enrollments found for the selected class.");
                    TempData["no"] = "No Student In classs";
                    ViewBag.ClassId = new SelectList(_context.Classes, "ClassId", "ClassId", model.ClassId);
                    return View(model);
                }

                var schedules = enrollments.Select(enrollment => new Schedule
                {
                    Day = model.Day,
                    SlotId = model.SlotId,
                    EnrollmentId = enrollment.EnrollmentId,
                    CourseId = model.CourseId,
                    RoomId = model.RoomId,
                    TeacherId = model.TeacherId,
                }).ToList();

                _context.Schedules.AddRange(schedules);
                await _context.SaveChangesAsync();
                TempData["ok"] = "Add successful!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "Unable to save changes. The student was updated or deleted by another user.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while saving changes: {ex.Message}");
            }

            ViewBag.ClassName = new SelectList(await _context.Classes
                .Select(c => new { c.ClassId, c.ClassName })
                .ToListAsync(), "ClassId", "ClassName", model.ClassId);
            ViewBag.RoomNumber = new SelectList(_context.Rooms, "RoomId", "RoomNumber", model.RoomId);
            ViewBag.CourseName = new SelectList(_context.Courses, "CourseId", "CourseName", model.CourseId);
            ViewBag.SlotName = new SelectList(_context.Slots, "SlotId", "SlotName", model.SlotId);
            ViewBag.Name = new SelectList(_context.Teachers, "TeacherId", "Name", model.TeacherId);

            return View(model);
        }

        #endregion

        #region View Schedule Student
        [Authorize(Roles = "Students")]
        [HttpGet]
        public async Task<IActionResult> ViewScheduleST(int page = 1)
        {
            const int DaysPerPage = 7;
            var today = DateTime.Today;
            var startDate = today.AddDays((page - 1) * DaysPerPage);
            var daysOfWeek = Enumerable.Range(0, DaysPerPage)
                .Select(i => startDate.AddDays(i))
                .ToArray();

            var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(studentId, out int parsedStudentId))
            {
                return View("Error"); // Trả về trang lỗi nếu studentId không hợp lệ
            }

            var slots = await _context.Slots.ToListAsync();
            var schedules = await _context.Schedules
                .Where(s => s.Enrollments.StudentId == parsedStudentId)
                .Include(s => s.Courses)
                .Include(s => s.Rooms)
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Class)
                .Include(s => s.Teachers)
                .Select(s => new ScheduleVM
                {
                    Day = s.Day,
                    SlotId = s.SlotId,
                    SlotName = s.Slots.SlotName,
                    ClassName = s.Enrollments.Class.ClassName,
                    CourseName = s.Courses.CourseName,
                    RoomNumber = s.Rooms.RoomNumber,
                    Name = s.Teachers.Name
                }).ToListAsync();


            // Log dữ liệu để kiểm tra
            Console.WriteLine("StudentId: " + parsedStudentId);
            Console.WriteLine("Number of Slots: " + slots.Count);
            Console.WriteLine("Number of Schedules: " + schedules.Count);

            var model = new ScheduleVM
            {
                DaysOfWeek = daysOfWeek.Select(d => d.ToString("yyyy-MM-dd")).ToArray(),
                Slots = slots,
                Schedules = schedules,
                CurrentPage = page
            };

            return View(model);
        }

        #endregion

        #region View Schedule Teacher
        [Authorize(Roles = "Teachers")]
        [HttpGet]
        public async Task<IActionResult> ViewScheduleTE(int page = 1)
        {
            const int DaysPerPage = 7;
            var today = DateTime.Today;
            var startDate = today.AddDays((page - 1) * DaysPerPage);
            var daysOfWeek = Enumerable.Range(0, DaysPerPage)
                .Select(i => startDate.AddDays(i))
                .ToArray();

            var teacherId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(teacherId, out int parsedTeacherId))
            {
                return View("Error"); 
            }

            var slots = await _context.Slots.ToListAsync();
            var schedules = await _context.Schedules
                .Where(s => s.TeacherId == parsedTeacherId)
                .Include(s => s.Courses)
                .Include(s => s.Rooms)
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Class)
                .Include(s => s.Teachers)
                .Select(s => new ScheduleVM
                {
                    Day = s.Day,
                    SlotId = s.SlotId,
                    SlotName = s.Slots.SlotName,
                    ClassName = s.Enrollments.Class.ClassName,
                    CourseName = s.Courses.CourseName,
                    RoomNumber = s.Rooms.RoomNumber,
                    Name = s.Teachers.Name,
                    TeacherId = s.TeacherId,                
                }).ToListAsync();


            // Log dữ liệu để kiểm tra
            Console.WriteLine("TeacherId: " + parsedTeacherId);
            Console.WriteLine("Number of Slots: " + slots.Count);
            Console.WriteLine("Number of Schedules: " + schedules.Count);

            var model = new ScheduleVM
            {
                DaysOfWeek = daysOfWeek.Select(d => d.ToString("yyyy-MM-dd")).ToArray(),
                Slots = slots,
                Schedules = schedules,
                CurrentPage = page
            };

            return View(model);
        }

        #endregion
    }
}
