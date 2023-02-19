using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace journalapp;

public partial class Emp : IdentityUser
{

    public string? Surname { get; set; }

    public string? Name { get; set; }

    public string? Patronymic { get; set; }

    public virtual ICollection<EducativeEvent> EducativeEvents { get; } = new List<EducativeEvent>();

    public virtual ICollection<Group> Groups { get; } = new List<Group>();
}
