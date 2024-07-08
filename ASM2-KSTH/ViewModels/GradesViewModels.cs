using ASM2_KSTH.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASM2_KSTH.ViewModels
{
    public class GradesViewModels
    {
        public int GradeId { get; set; }

     
        public decimal? Grade1 { get; set; }

        [Required]
        public int SelectedStudentId { get; set; }

        [Required]
        public int SelectedCourseId { get; set; }

        [Required]
        public int SelectedEnrollmentId { get; set; }

		public string StudentName { get; set; }

        public string CourseName { get; set; }


		public List<Student> Students { get; set; }  // Danh sách học sinh để chọn
        public List<Course> Courses { get; set; }    // Danh sách các môn học để chọn
        public List<Class> Classes { get; set; }     // Danh sách các lớp học để chọn
    }

    public class StudentGradesViewModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public List<GradesViewModels> Grades { get; set; }
    }
}
