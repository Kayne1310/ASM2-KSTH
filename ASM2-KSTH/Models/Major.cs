using ASM2_KSTH.ViewModels;

namespace ASM2_KSTH.Models
{
    public class Major
    {
        public int MajorId { get; set; } // Primary Key
        public string? MajorName { get; set; }
        public ICollection<StudentRegister>? Students { get; set; }
        public ICollection<Course>? Courses { get; set; }
    }

}
