using System;


namespace ASM2_KSTH.ViewModels
{
    public class StudentViewModel
    {

        public int StudentId { get; set; }
        public string? Name { get; set; }
        public DateOnly DateOfBirth { get; set; } = DateOnly.MinValue;
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? MajorName { get; set; }
        public decimal? Grade1 { get; set; }

        public string? CourseName {  get; set; }
        public string? ClassName { get; set; }
    }
}
