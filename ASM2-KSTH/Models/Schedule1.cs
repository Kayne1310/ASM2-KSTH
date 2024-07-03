using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASM2_KSTH.Models;

public partial class Schedule1
{
    [Key]
    public int ScheduleId { get; set; }

    public int ClassId { get; set; }

    public int StudentId { get; set; }

    public int CourseId { get; set; }

    public int RoomId { get; set; }

    public DateOnly Day { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;
}
