using System;
using System.Collections.Generic;
using journalapp.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace journalapp;

public partial class Emp : IdentityUser
{

    public string? Surname { get; set; }

    public string? Name { get; set; }

    public string? Patronymic { get; set; }

    public virtual ICollection<Group> Groups { get; } = new List<Group>();

        public virtual ICollection<InteractionWithTeachers> InteractionsWithTeachers { get; } = new List<InteractionWithTeachers>();

      public string GetShortName(){
        string shortName;
        if (Patronymic !=null)
        shortName= String.Concat(Surname, " ", Name.Substring(0, 1), ". ", Patronymic.Substring(0, 1), ".");
        else
        shortName= String.Concat(Surname, " ", Name.Substring(0, 1), ".");
        return shortName;
    }

    public string GetFullName(){
        string fullName;
        if (Patronymic !=null)
        fullName= String.Concat(Surname, " ", Name, " ", Patronymic);
        else
        fullName= String.Concat(Surname, " ", Name);
        return fullName;
    }
}
