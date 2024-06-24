using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASM2_KSTH.Models
{
    public class NumSession
    {
        [Key]
        
        public int NumId { get; set; }

        [Required]
        public int Numses { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }

        public virtual Course? Course { get; set; }
    }
}
