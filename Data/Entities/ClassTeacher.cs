using System;
using System.Collections.Generic;

namespace journalapp;

public partial class ClassTeacher
{
    public int Id { get; set; }

    public string? Surname { get; set; }

    public string? Name { get; set; }

    public string? Patronymic { get; set; }

    public virtual ICollection<AspNetUser> AspNetUsers { get; } = new List<AspNetUser>();

    public virtual ICollection<EducativeEvent> EducativeEvents { get; } = new List<EducativeEvent>();

    public virtual ICollection<Group> Groups { get; } = new List<Group>();
}
