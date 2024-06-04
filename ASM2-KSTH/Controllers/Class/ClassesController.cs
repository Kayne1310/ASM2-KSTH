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
using Microsoft.AspNetCore.Authorization;

namespace ASM2_KSTH.Controllers
{
    public class ClassesController : Controller
    {
        private readonly ASM2_KSTHContext _context;

        public ClassesController(ASM2_KSTHContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var classEntities = await _context.Classes
                .Include(c => c.Room)
                .Include(c => c.Teacher)
                .Include(c => c.Course)
                .Include(c => c.Enrollments)
                .ThenInclude(e => e.Student)
                .ToListAsync();

            var viewModelList = classEntities.Select(classEntity => new ClassViewModel
            {
                ClassId = classEntity.ClassId,
                ClassName = classEntity.ClassName,
                Semester = classEntity.Semester,
                Year = classEntity.Year,
                Room = classEntity.Room?.RoomNumber,
                Teacher = classEntity.Teacher?.Name,
                StudentCount = classEntity.Enrollments.Count(e => e.Student != null),
                CourseName = classEntity.Course?.CourseName
            }).ToList();


            return View(viewModelList);
        }
        


        public async Task<IActionResult> Edit(int? classId)
        {
            if (classId == null)
            {
                return NotFound();
            }

            var classEntity = await _context.Classes
                .Include(c => c.Room)
                .Include(c => c.Teacher)
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.ClassId == classId);

            if (classEntity == null)
            {
                return NotFound();
            }

            var viewModel = new ClassViewModel
            {
                ClassId = classEntity.ClassId,
                ClassName = classEntity.ClassName,
                Semester = classEntity.Semester,
                Year = classEntity.Year,
                Room = classEntity.Room?.RoomNumber,
                Teacher = classEntity.Teacher?.Name,
                CourseName = classEntity.Course?.CourseName
            };
            ViewBag.Rooms = new SelectList(await _context.Rooms.ToListAsync(), "RoomNumber", "RoomNumber", classEntity.Room?.RoomNumber);
            ViewBag.Teachers = new SelectList(await _context.Teachers.ToListAsync(), "Name", "Name", classEntity.Teacher?.Name);
            ViewBag.Courses = new SelectList(await _context.Courses.ToListAsync(), "CourseName", "CourseName", classEntity.Course?.CourseName);
            ViewBag.Semesters = new SelectList(new List<string> { "Spring", "Summer", "Fall", "Winter" }, classEntity.Semester);


            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int classId, ClassViewModel viewModel)
        {
            if (classId != viewModel.ClassId)
            {
                return NotFound();
            }

            var classEntity = await _context.Classes
                .Include(c => c.Room)
                .Include(c => c.Teacher)
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.ClassId == classId);

            if (classEntity == null)
            {
                return NotFound();
            }

            classEntity.ClassName = viewModel.ClassName;
            classEntity.Semester = viewModel.Semester;
            classEntity.Year = viewModel.Year;
            classEntity.Room = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomNumber == viewModel.Room);
            classEntity.Teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Name == viewModel.Teacher);
            classEntity.Course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseName == viewModel.CourseName);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassExists(classEntity.ClassId))
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

            ViewBag.Rooms = new SelectList(await _context.Rooms.ToListAsync(), "RoomNumber", "RoomNumber", viewModel.Room);
            ViewBag.Teachers = new SelectList(await _context.Teachers.ToListAsync(), "Name", "Name", viewModel.Teacher);
            ViewBag.Courses = new SelectList(await _context.Courses.ToListAsync(), "CourseName", "CourseName", viewModel.CourseName);
            ViewBag.Semesters = new SelectList(new List<string> { "Spring", "Summer", "Fall", "Winter" }, viewModel.Semester);
            return View(viewModel);
        }

        //// GET: Classes/Delete/5
        //public async Task<IActionResult> Delete(int? classId)
        //{
        //    if (classId == null)
        //    {
        //        return NotFound();
        //    }

        //    var classEntity = await _context.Classes
        //       .Include(c => c.Room)
        //       .Include(c => c.Teacher)
        //       .Include(c => c.Course)
        //       .FirstOrDefaultAsync(m => m.ClassId == classId);

        //    if (classEntity == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["ClassId"] = classEntity.ClassId;

        //    var viewModel = new ClassViewModel
        //    {
        //        ClassName = classEntity.ClassName,
        //        Semester = classEntity.Semester,
        //        Year = classEntity.Year,
        //        Room = classEntity.Room?.RoomNumber,
        //        Teacher = classEntity.Teacher?.Name,
        //        CourseName = classEntity.Course?.CourseName
        //    };

        //    return View(viewModel);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]

        //public async Task<IActionResult> Delete(int classId)
        //{


        //    var classEntity = await _context.Classes.FindAsync(classId);
        //    if (classEntity == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Classes.Remove(classEntity);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Populate ViewBag with the necessary data for dropdown lists
            ViewBag.Rooms = new SelectList(await _context.Rooms.ToListAsync(), "RoomNumber", "RoomNumber");
            ViewBag.Teachers = new SelectList(await _context.Teachers.ToListAsync(), "Name", "Name");
            ViewBag.Courses = new SelectList(await _context.Courses.ToListAsync(), "CourseName", "CourseName");
            ViewBag.Semesters = new SelectList(new List<string> { "Spring", "Summer", "Fall", "Winter" });

            // Return the view with the ViewModel
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClassViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Check if ClassName already exists
                if (await ClassNameExists(viewModel.ClassName))
                {
                    ModelState.AddModelError("ClassName", "Class name already exists.");
                    // Re-populate ViewBag with dropdown list data
                    ViewBag.Rooms = new SelectList(await _context.Rooms.ToListAsync(), "RoomNumber", "RoomNumber");
                    ViewBag.Teachers = new SelectList(await _context.Teachers.ToListAsync(), "Name", "Name");
                    ViewBag.Courses = new SelectList(await _context.Courses.ToListAsync(), "CourseName", "CourseName");
                    ViewBag.Semesters = new SelectList(new List<string> { "Spring", "Summer", "Fall", "Winter" });
                    return View(viewModel);
                }

                // Retrieve the room entity based on the selected room number
                var room = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomNumber == viewModel.Room);
             
                // Retrieve the course entity based on the selected course name
                var course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseName == viewModel.CourseName);

                // Retrieve the teacher entity based on the selected teacher name
                var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Name == viewModel.Teacher);

                // Check if room, course, and teacher are found
                if (room != null && course != null && teacher != null)
                {
                    // Create a new Class entity and populate its properties
                    var newClass = new Class
                    {
                        ClassName = viewModel.ClassName,
                        Semester = viewModel.Semester,
                        Year = viewModel.Year,
                        Room = room,
                        Course = course,
                        Teacher = teacher
                    };

                    // Add the new Class entity to the context and save changes
                    _context.Classes.Add(newClass);
                    await _context.SaveChangesAsync();

                    // Redirect to Index action after successful creation
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // If room, course, or teacher is not found, return a 404 NotFound view
                    return NotFound();
                }
            }

            // If model state is not valid, return the view with validation errors
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ListRoom() 
        {
            var rooms = await _context.Rooms.ToListAsync();
            return View(rooms);
        }


        [HttpGet]
        public async Task<IActionResult> CreateRoom()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoom(Room room)
        {

            if (ModelState.IsValid)
            {
                _context.Rooms.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListRoom));
            }

            return View(room);
        }



        private async Task<bool> ClassNameExists(string className)
        {
            // Check if a class with the same ClassName already exists in the database
            return await _context.Classes.AnyAsync(c => c.ClassName == className);
        }

       

        private bool ClassExists(int id)
        {
            return _context.Classes.Any(e => e.ClassId == id);
        }

    }
}
