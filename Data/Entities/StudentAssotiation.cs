using System;
using System.Collections.Generic;

namespace journalapp;

public partial class StudentAssotiation
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Business> Businesses { get; } = new List<Business>();
}
