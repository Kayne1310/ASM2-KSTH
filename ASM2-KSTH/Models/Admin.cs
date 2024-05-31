using System.ComponentModel.DataAnnotations;

namespace ASM2_KSTH.Models
{
    public class Admin
    { 
        public int Id { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required")]
        [MinLength(5, ErrorMessage = "Username must be at least 5 characters")]
        public string? Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

    }
}
