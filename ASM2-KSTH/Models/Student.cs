using System;
using System.Collections.Generic;

namespace ASM2_KSTH.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string? Name { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public int? MajorId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual Major? Major { get; set; }
}
