using System;
using System.Collections.Generic;

namespace journalapp;

public partial class HealthGroup
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Recommendation { get; set; }

    public virtual ICollection<Student> Students { get; } = new List<Student>();
}
