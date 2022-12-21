using System;
using System.Collections.Generic;

namespace journalapp;

public partial class Passport
{
    public int Id { get; set; }

    public int? CountStudMen { get; set; }

    public int? ContStudWomen { get; set; }

    public int? CountStudents { get; set; }

    public int? CountPedControl { get; set; }

    public int? CountWorkOfStud { get; set; }

    public int? CountWorkOfParents { get; set; }

    public int? CountOrphans { get; set; }

    public int? CountPdnstud { get; set; }

    public int? CountOvzstud { get; set; }

    public int? CountHostelVisits { get; set; }

    public int? CountCommunHours { get; set; }

    public int? CountParentsMeetings { get; set; }

    public int? CountStudInEvents { get; set; }

    public string GroupId { get; set; } = null!;

    public DateTime Date { get; set; }

    public virtual Group Group { get; set; } = null!;
}
