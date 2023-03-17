using System;
using System.Collections.Generic;

namespace journalapp;

public partial class CommunicationHour
{
    public int Id { get; set; }

    public int Semestr { get; set; }

    public DateTime Date { get; set; }

    public string Theme { get; set; } = null!;

    public string? Result { get; set; }

    public int? StudCount { get; set; }

    public string GroupId { get; set; } = null!;

    public virtual Group Group { get; set; } = null!;
}
