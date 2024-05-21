using System.ComponentModel.DataAnnotations;

namespace ASM2_KSTH.Models
{
    public class Test
    {
        [Key]
        public int id { get; set; }

        public string Ten { get; set; }
        public string Title { get; set; }
    }
}
