using System;
using System.Collections.Generic;

namespace ASM2_KSTH.Models;

public partial class Teacher
{
    public int TeacherId { get; set; }

    public string? Name { get; set; }

    public string? Department { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
