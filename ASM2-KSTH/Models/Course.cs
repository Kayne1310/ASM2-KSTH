using System;
using System.Collections.Generic;

namespace ASM2_KSTH.Models;

public partial class Course
{

    public int CourseId { get; set; }

    public string? CourseName { get; set; }

    public string? CourseDescription { get; set; }

    public int? Credits { get; set; }

    public int? MajorId { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual Major? Major { get; set; }
}
