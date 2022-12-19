using System;
using System.Collections.Generic;

namespace journalapp;

public partial class EducativeEvent
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Lobid { get; set; }

    public string ClassTeacherId { get; set; }

    public DateTime Date { get; set; }

    public virtual ClassTeacher ClassTeacher { get; set; } = null!;

    public virtual LineOfBusiness Lob { get; set; } = null!;
}
