using System;
using System.Collections.Generic;

namespace journalapp;

public partial class StudentsOnPedControl
{
    public int Id { get; set; }

    public int Semestr { get; set; }

    public int StudentId { get; set; }

    public int Tocid { get; set; }

    public DateTime Date { get; set; }

    public string MeasuresTaken { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;

    public virtual TypeOfCrime Toc { get; set; } = null!;
}
