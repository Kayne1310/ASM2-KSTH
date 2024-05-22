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

        public ICollection<Class>? Classes { get; set; }
    }
}
