using System;
using System.Collections.Generic;

namespace journalapp;

public partial class RiskGroup
{
    public int Id { get; set; }

    public string Reason { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
