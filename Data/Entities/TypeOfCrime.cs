using System;
using System.Collections.Generic;

namespace journalapp;

public partial class TypeOfCrime
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<StudentsOnPedControl> StudentsOnPedControls { get; } = new List<StudentsOnPedControl>();
}
