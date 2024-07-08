using System;


namespace ASM2_KSTH.ViewModels
{
    public class StudentViewModel
    {

        public int StudentId { get; set; }
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; } = DateOnly.MinValue;
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? MajorName { get; set; }
        public decimal? Grade1 { get; set; }
        public string AttendanceStatus { get; set; }

        public string? Reason { get; set; }

        public DateTime? AttendanceDate { get; set; }

        public int ? TeacherId { get; set; }
        public int ? RoomId { get; set; }


        public List<NumSessionViewModel> Numses { get; set; }
        public int? GradeId { get; set; }
        public string? CourseName {  get; set; }
        public string? ClassName { get; set; }
        public int CourseId { get; set; } // Thêm trường CourseId
        public int EnrollmentId { get; set; } // Thêm trường EnrollmentId

        public int ? NumId { get; set; }
    }
    public class NumSessionViewModel
    {
        public int NumId { get; set; }
        public string Numses { get; set; }
    }

}
