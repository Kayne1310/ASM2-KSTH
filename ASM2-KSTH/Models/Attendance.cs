using System;
using System.Collections.Generic;

namespace ASM2_KSTH.Models;

public partial class Attendance
{
    public int Id { get; set; }

    public int? StudentId { get; set; }

    public int? ClassId { get; set; }

    public int? EnrollmentId { get; set; }

    public int? TeacherId { get; set; }

    public int? RoomId { get; set; }

    public DateOnly AttendanceDate { get; set; }

    public string AttendanceStatus { get; set; } = null!;

    public string? Reason { get; set; }

    public int? NumId { get; set; }

    public virtual Class? Class { get; set; }

    public virtual Enrollment? Enrollment { get; set; }

    public virtual Room? Room { get; set; }

    public virtual Student? Student { get; set; }

    public virtual Teacher? Teacher { get; set; }




}
