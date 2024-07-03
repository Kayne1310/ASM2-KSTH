using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASM2_KSTH.Models;

public partial class NumSession
{
    [Key]
    public int NumId { get; set; }

    public string Numses { get; set; }

    public int? CourseId { get; set; }

    public virtual Course? Course { get; set; }


}
 