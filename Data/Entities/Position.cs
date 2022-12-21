using System;
using System.Collections.Generic;

namespace journalapp;

public partial class Position
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? StudentId { get; set; }

    public virtual Student? Student { get; set; }
}
