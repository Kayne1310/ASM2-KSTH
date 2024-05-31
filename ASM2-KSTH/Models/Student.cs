using System.ComponentModel.DataAnnotations;

namespace ASM2_KSTH.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string? Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int MajorId { get; set; } // Foreign Key
        public Major? Major { get; set; }
        public string? RandomKey { get; set; }

        [Display(Name = "Username")]
		[Required(ErrorMessage = "Username is required")]
		[MinLength(5, ErrorMessage = "Username must be at least 6 characters")]
		public string? Username { get; set; }

		[Display(Name = "Password")]
		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string? Password { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();


    }

}
