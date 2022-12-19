using System;
using System.Collections.Generic;

namespace journalapp;

public partial class Hostel
{
    public int Id { get; set; }

    public string Address { get; set; } = null!;

    public virtual ICollection<Room> Rooms { get; } = new List<Room>();

    public virtual ICollection<Room> RoomsNavigation { get; } = new List<Room>();
}
