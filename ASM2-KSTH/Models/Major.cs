namespace ASM2_KSTH.Models
{
    public class Major
    {
        public int MajorId { get; set; } // Primary Key
        public string? MajorName { get; set; }

        public ICollection<Student>? Students { get; set; }
        public ICollection<Course>? Courses { get; set; }
    }
}
