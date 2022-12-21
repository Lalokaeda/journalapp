using System;
using System.Collections.Generic;

namespace journalapp;

public partial class CourseOfGroup
{
    public string GroupId { get; set; } = null!;

    public int Course { get; set; }

    public DateTime Year { get; set; }

    public virtual Group Group { get; set; } = null!;
}
