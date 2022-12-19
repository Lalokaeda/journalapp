using System;
using System.Collections.Generic;

namespace journalapp;

public partial class Division
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Speciality> Specialities { get; } = new List<Speciality>();
}
