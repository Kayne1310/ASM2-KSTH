
﻿using ASM2_KSTH.ViewModels;
using System.Diagnostics;
using System.Collections.Generic;

namespace ASM2_KSTH.Models;

public partial class Enrollment
{
    public int Id { get; set; }

    public int? StudentId { get; set; }

    public int? ClassId { get; set; }

    public virtual Class? Class { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual Student? Student { get; set; }
}
