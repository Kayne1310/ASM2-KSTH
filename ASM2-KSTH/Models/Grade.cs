namespace ASM2_KSTH.Models
{
    public class Grade
    {
        public int GradeId { get; set; } // Primary Key
        public int EnrollmentId { get; set; } // Foreign Key
        public string? GradeValue { get; set; }
        public DateTime GradeDate { get; set; }

        public Enrollment ? Enrollment { get; set; }
    }
}
