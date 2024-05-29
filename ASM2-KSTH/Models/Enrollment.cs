using ASM2_KSTH.ViewModels;
using System.Diagnostics;

namespace ASM2_KSTH.Models
{
    public class Enrollment
    {
        public int Id { get; set; } // Primary Key
        public int StudentId { get; set; } // Foreign Key
        public int ClassId { get; set; } // Foreign Key

        public StudentRegister? Student { get; set; }
        public Class? Class { get; set; }
        public ICollection<Grade>? Grades { get; set; }
    }
}
