using System.ComponentModel.DataAnnotations;

namespace ASM2_KSTH.Models
{
    public class Slot
    {
        [Key]
        public int SlotId { get; set; }
        public string SlotName { get; set; }

        [DataType(DataType.Time)]
      
        public TimeSpan StartTime { get; set; }

        [DataType(DataType.Time)]
     
        public TimeSpan EndTime { get; set; }
    }
}
