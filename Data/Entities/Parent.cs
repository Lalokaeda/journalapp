using System;
using System.Collections.Generic;

namespace journalapp;

public partial class Parent
{
    public int Id { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string PhoneNum { get; set; } = null!;

    public string? Email { get; set; }

    public virtual ICollection<WorkWithParent> WorkWithParents { get; } = new List<WorkWithParent>();

    public virtual ICollection<Student> Srudents { get; } = new List<Student>();
}
