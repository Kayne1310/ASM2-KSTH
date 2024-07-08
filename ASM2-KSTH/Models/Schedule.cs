using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM2_KSTH.Models
{
	public class Schedule
	{
		[Key]
		public int ScheduleId { get; set; }

		[DataType(DataType.Date)]
		public DateTime Day { get; set; }
		public int SlotId {  get; set; }
		public virtual Slot Slots { get; set; }
		public int EnrollmentId { get; set; }
		public virtual Enrollment Enrollments { get; set; }
		public int CourseId { get; set; }
		public virtual Course Courses { get; set; }
		public int RoomId { get; set; }	
		public virtual Room Rooms { get; set; }
		public int? TeacherId { get; set; }
		public virtual Teacher Teachers { get;	set; }

    }
}
