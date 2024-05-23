using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace ASM2_KSTH.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; } // Primary Key
        public string? Name { get; set; }
        public string? Department { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "*")]
        [MinLength(6, ErrorMessage = "Username must be at least 6 characters")]
        public string? Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public ICollection<Class>? Classes { get; set; }
    }
}
