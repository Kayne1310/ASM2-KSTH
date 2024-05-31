
using ASM2_KSTH.Models;
using System.ComponentModel.DataAnnotations;

namespace ASM2_KSTH.ViewModels

{
    public class StudentRegister
	{
        [Key]
        public int StudentId { get; set; } // Primary Key

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

		[Display(Name = "Date Of Birth")]
		[Required(ErrorMessage = "Date Of Birth is required")]
		[DataType(DataType.Date)]
		public DateTime? DateOfBirth { get; set; }

		[Display(Name = "Address")]
		[Required(ErrorMessage = "Address is required")]
		[MaxLength(30, ErrorMessage = "Maximum 30 characters")]
		public string Address { get; set; }

		[Display(Name = "Phone number")]
		[Required(ErrorMessage = "Phone number is required")]
		[MaxLength(10, ErrorMessage = "Maximum 10 characters")]
		[RegularExpression(@"0[982]\d{8}", ErrorMessage = "+84 format is not correct")]
		public string PhoneNumber { get; set; }

		[Display(Name = "Email")]
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Incorrect email format")]
		public string Email { get; set; }
		public int MajorId { get; set; } // Foreign Key

		[Required(ErrorMessage = "MajorName is required")]
		public string MajorName { get; set; }
		public Major Major { get; set; }

		[Display(Name = "Username")]
		[Required(ErrorMessage = "Username is required")]
		[MaxLength(20, ErrorMessage = "Username must be at least 20 characters")]
		public string Username { get; set; }

		[Display(Name = "Password")]
		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public ICollection<Enrollment>? Enrollments { get; set; }
	}
}
