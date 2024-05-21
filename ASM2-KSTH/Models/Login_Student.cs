using System.ComponentModel.DataAnnotations;

namespace ASM2_KSTH.Models
{
    public class Login_Student
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Username {  get; set; } 
        public string? Password { get; set; }
    }
}
