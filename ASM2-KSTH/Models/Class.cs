using System;
using System.Collections.Generic;

namespace ASM2_KSTH.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public int? CourseId { get; set; }

    public int? TeacherId { get; set; }

    public string? Semester { get; set; }

    public int Year { get; set; }

    public int? RoomId { get; set; }

    public string? ClassName { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Room? Room { get; set; }

    public virtual Teacher? Teacher { get; set; }
    public virtual ICollection<Attendance> Attendance { get; set; } = new List<Attendance>();
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    public virtual ICollection<Schedule1> Schedule1s { get; set; } = new List<Schedule1>();
   
}
