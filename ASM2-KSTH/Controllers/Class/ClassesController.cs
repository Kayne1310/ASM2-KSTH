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
        [HttpPost]
        public async Task<IActionResult> Create(int id)
        {
            return View();
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

            return View(viewModel);
        }



        // POST: Classes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int classId, ClassViewModel viewModel)
        {
            if (classId != viewModel.ClassId)
            {
                return NotFound();
            }

            var classEntity = await _context.Classes.FindAsync(classId);
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
            return View(viewModel);
        }

        // GET: Classes/Delete/5
        public async Task<IActionResult> Delete(int? classId)
        {
            if (classId == null)
            {
                return NotFound();
            }

            var classEntity = await _context.Classes
                .FirstOrDefaultAsync(m => m.ClassId ==classId);
            if (classEntity == null)
            {
                return NotFound();
            }

            var viewModel = new ClassViewModel
            {
                ClassName = classEntity.ClassName,
                Semester = classEntity.Semester,
                Year = classEntity.Year,
                Room = classEntity.Room?.RoomNumber,
                Teacher = classEntity.Teacher?.Name,
                CourseName = classEntity.Course?.CourseName
            };

            return View(viewModel);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classEntity = await _context.Classes.FindAsync(id);
            _context.Classes.Remove(classEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassExists(int id)
        {
            return _context.Classes.Any(e => e.ClassId == id);
        }

    }
}
