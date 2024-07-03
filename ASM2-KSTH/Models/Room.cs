using System;
using System.Collections.Generic;

namespace ASM2_KSTH.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public string? RoomNumber { get; set; }

    public virtual ICollection<Attendance> Attendance { get; set; } = new List<Attendance>();
  

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Schedule1> Schedule1s { get; set; } = new List<Schedule1>();
}
