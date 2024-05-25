using System;
using System.Collections.Generic;

namespace ASM2_KSTH.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public string? RoomNumber { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
