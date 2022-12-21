using System;
using System.Collections.Generic;

namespace journalapp;

public partial class Student
{
    public int Id { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string Sex { get; set; } = null!;

    public int? HealthGroupId { get; set; }

    public string PhoneNum { get; set; } = null!;

    public string? Email { get; set; }

    public string Address { get; set; } = null!;

    public string GroupId { get; set; } = null!;

    public bool IsCommerce { get; set; }

    public bool IsExpelled { get; set; }

    public int? RoomId { get; set; }

    public string? Note { get; set; }

    public virtual ICollection<Business> Businesses { get; } = new List<Business>();

    public virtual ICollection<Curator> Curators { get; } = new List<Curator>();

    public virtual ICollection<GraphicVisitsHostel> GraphicVisitsHostels { get; } = new List<GraphicVisitsHostel>();

    public virtual Group Group { get; set; } = null!;

    public virtual HealthGroup? HealthGroup { get; set; }

    public virtual ICollection<Position> Positions { get; } = new List<Position>();

    public virtual Room? Room { get; set; }

    public virtual ICollection<StudentsOfEvent> StudentsOfEvents { get; } = new List<StudentsOfEvent>();

    public virtual ICollection<StudentsOnPedControl> StudentsOnPedControls { get; } = new List<StudentsOnPedControl>();

    public virtual ICollection<WorkWithStudent> WorkWithStudents { get; } = new List<WorkWithStudent>();

    public virtual ICollection<Parent> Parents { get; } = new List<Parent>();

    public virtual ICollection<RiskGroup> Reasons { get; } = new List<RiskGroup>();
}
