using System;
using System.Collections.Generic;

namespace journalapp;

public partial class Curator
{
    public int Id { get; set; }

    public int? StudentId { get; set; }

    public string? ChildGroupId { get; set; }

    public virtual Group? ChildGroup { get; set; }

    public virtual Student? Student { get; set; }
}
