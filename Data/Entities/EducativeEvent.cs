using System;
using System.Collections.Generic;

namespace journalapp;

public partial class EducativeEvent
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Lobid { get; set; }

    public string EmpId { get; set; }

    public DateTime Date { get; set; }

    public virtual Emp Emp { get; set; } = null!;

    public virtual LineOfBusiness Lob { get; set; } = null!;
}
