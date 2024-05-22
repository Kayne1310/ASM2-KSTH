namespace ASM2_KSTH.Models
{
    public class Student
    {
        public int StudentId { get; set; } // Primary Key
        public string? Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int MajorId { get; set; } // Foreign Key

        public Major? Major { get; set; }
        public ICollection<Enrollments>? Enrollments { get; set; }
    }

}
