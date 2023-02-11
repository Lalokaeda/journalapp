using System;
using System.Collections.Generic;

namespace journalapp;

public partial class Position
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public virtual ICollection<Student> Students { get; } = new List<Student>();
}
