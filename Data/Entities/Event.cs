using System;
using System.Collections.Generic;

namespace journalapp;

public partial class Event
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<StudentsOfEvent> StudentsOfEvents { get; } = new List<StudentsOfEvent>();
}
