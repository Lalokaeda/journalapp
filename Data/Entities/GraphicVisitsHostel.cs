using System;
using System.Collections.Generic;

namespace journalapp;

public partial class GraphicVisitsHostel
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int Semestr {get; set;}
    public DateTime VisitDate { get; set; }

    public string GoalOfVisit { get; set; } = null!;

    public string Result { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
