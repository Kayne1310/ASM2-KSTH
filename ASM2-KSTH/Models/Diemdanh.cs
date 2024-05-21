using System.ComponentModel.DataAnnotations;

namespace ASM2_KSTH.Models
{
    public class Diemdanh
    {
        [Key]
        public int id   { get; set; }

        public string tenlop { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
