using System;
using System.Collections.Generic;

namespace ASM2_KSTH.Models;

public partial class Major
{
    public int MajorId { get; set; }

    public string? MajorName { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
