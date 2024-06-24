using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASM2_KSTH.Models
{

    public class Schedule
    {
        [Key]
        public int ScheduleID { get; set; }

        [ForeignKey("Class")]
        public int ClassId { get; set; }

        public virtual Class? Class{ get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        
        [ForeignKey("Room")]
        public int RoomId { get; set; }

        public virtual Course? Course { get; set; }
        public virtual Room? Room { get; set; }

        [DataType(DataType.Date)]
        public DateTime Day { get; set; }


        [DataType(DataType.Time)]

        public TimeSpan StartTime { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }

    }
}
