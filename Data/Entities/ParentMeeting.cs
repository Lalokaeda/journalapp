using System;
using System.Collections.Generic;

namespace journalapp;

public partial class ParentMeeting
{
    public int Id { get; set; }

    public DateTime? Date { get; set; }

    public string? Theme { get; set; }

    public string? Result { get; set; }

    public int? ParentsCount { get; set; }

    public int Semestr { get; set; }

    public string GroupId { get; set; }=null!;

    public virtual Group Group { get; set; } = null!;

}
