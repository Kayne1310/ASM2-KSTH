using System.Diagnostics;

namespace ASM2_KSTH.Models
{
    public class Enrollments
    {
        public int Id { get; set; } // Primary Key
        public int StudentId { get; set; } // Foreign Key
        public int ClassId { get; set; } // Foreign Key

        public Student  ? Student { get; set; }
        public Class? Class { get; set; }
        public ICollection<Grade>? Grades { get; set; }
    }
}
