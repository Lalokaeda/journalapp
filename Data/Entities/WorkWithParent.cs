using System;
using System.Collections.Generic;

namespace journalapp;

public partial class WorkWithParent
{
    public int Id { get; set; }

    public int ParentId { get; set; }

    public string Questions { get; set; } = null!;

    public DateTime Date { get; set; }

    public virtual Parent Parent { get; set; } = null!;
}
