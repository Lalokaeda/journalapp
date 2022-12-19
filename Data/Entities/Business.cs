using System;
using System.Collections.Generic;

namespace journalapp;

public partial class Business
{
    public int Id { get; set; }

    public DateTime Year { get; set; }

    public int Semester { get; set; }

    public int StudentId { get; set; }

    public string? Workshop { get; set; }

    public int? StudentAssotiationId { get; set; }

    public virtual Student Student { get; set; } = null!;

    public virtual StudentAssotiation? StudentAssotiation { get; set; }
}
