using ASM2_KSTH.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM2_KSTH.ViewModels

{
    public class ScheduleVM
    {
        [Key]
        public int ScheduleId { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime Day { get; set; }
        public int SlotId { get; set; }
        public string SlotName { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public int? TeacherId { get; set; }
        public string Name { get; set; }
        public int EnrollmentId { get; set; }

        public string[] DaysOfWeek { get; set; }
        public int CurrentPage { get; set; }

        public IEnumerable<Slot> Slots { get; set; }
        public IEnumerable<ScheduleVM> Schedules { get; set; }
    }
}