using System;
using System.Collections.Generic;
using journalapp.Data.Entities;

namespace journalapp;

public partial class LineOfBusiness
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<EducativeEvent> EducativeEvents { get; } = new List<EducativeEvent>();
    public virtual ICollection<IndividualAchiv> IndividualAchivs { get; } = new List<IndividualAchiv>();
}
