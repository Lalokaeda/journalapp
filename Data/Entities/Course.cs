using System;
using System.Collections.Generic;

namespace journalapp;

public partial class Course
{
    public int Id { get; set; }

    public virtual ICollection<CourseOfGroup> CourseOfGroups { get; } = new List<CourseOfGroup>();
}
