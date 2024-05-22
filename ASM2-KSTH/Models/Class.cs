namespace ASM2_KSTH.Models
{
    public class Class
    {
        public int ClassId { get; set; } // Primary Key
        public int CourseId { get; set; } // Foreign Key
        public int TeacherId { get; set; } // Foreign Key
        public string ? Semester { get; set; }
        public int Year { get; set; }
        public int RoomId { get; set; } // Foreign Key

        public Course ? Course { get; set; }
        public Teacher ? Teacher { get; set; }
        public Room ? Room { get; set; }
        public ICollection<Enrollments>? Enrollments { get; set; }
    }
}
