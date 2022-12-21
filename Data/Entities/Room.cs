using System;
using System.Collections.Generic;

namespace journalapp;

public partial class Room
{
    public int Id { get; set; }

    public int NumofRoom { get; set; }

    public int Roominess { get; set; }

    public int HostelId { get; set; }

    public virtual Hostel Hostel { get; set; } = null!;

    public virtual ICollection<Student> Students { get; } = new List<Student>();
}
