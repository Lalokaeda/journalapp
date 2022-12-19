using System;
using System.Collections.Generic;

namespace journalapp;

public partial class StudentsOfEvent
{
    public int Id { get; set; }

    public DateTime? DateTime { get; set; }

    public int EventId { get; set; }

    public string Result { get; set; } = null!;

    public int StudentId { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
