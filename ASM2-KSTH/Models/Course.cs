using System.Security.Claims;

namespace ASM2_KSTH.Models
{
    public class Course
    {
        public int CourseId { get; set; } // Primary Key
        public string? CName { get; set; }
        public string? CourseDescription { get; set; }
        public int Credits { get; set; }
        public int MajorId { get; set; } // Foreign Key

        public Major ?Major { get; set; }
        public ICollection<Class>? Classes { get; set; }
    }
}
