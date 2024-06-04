using System;
using System.Collections.Generic;

namespace ASM2_KSTH.Models;

public partial class Grade
{
    public int GradeId { get; set; }

    public int? EnrollmentId { get; set; }

    public decimal? Grade1 { get; set; }

    public int? CourseId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Enrollment? Enrollment { get; set; }
}
