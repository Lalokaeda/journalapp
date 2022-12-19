using System;
using System.Collections.Generic;

namespace journalapp;

public partial class CourseOfGroup
{
    public string GroupId { get; set; } = null!;

    public int CourseId { get; set; }

    public DateTime Year { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Group Group { get; set; } = null!;
}
