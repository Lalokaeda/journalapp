using System;
using System.Collections.Generic;

namespace journalapp;

public partial class WorkWithStudent
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public string Reason { get; set; } = null!;

    public string Work { get; set; } = null!;

    public DateTime Date { get; set; }

    public virtual Student Student { get; set; } = null!;
}
