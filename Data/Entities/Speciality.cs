using System;
using System.Collections.Generic;

namespace journalapp;

public partial class Speciality
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int DivisionId { get; set; }

    public virtual Division Division { get; set; } = null!;

    public virtual ICollection<Group> Groups { get; } = new List<Group>();
}
