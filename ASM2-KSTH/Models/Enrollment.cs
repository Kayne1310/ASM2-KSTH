using System.Linq;
using ASM2_KSTH.ViewModels;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASM2_KSTH.Models;

public partial class Enrollment
{
    [Key]
    public int EnrollmentId { get; set; }

    public int ClassId { get; set; }

	public int? StudentId { get; set; }

	public virtual Class? Class { get; set; }

    public virtual Student? Student { get; set; }

	public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

}
