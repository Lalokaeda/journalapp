using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace journalapp;

public partial class Passport
{
    public int Id { get; set; }

    public int Semestr { get; set; }
    
    public string GroupId { get; set; } = null!;

    public int? CountStudMen { get; set; }

    public int? ContStudWomen { get; set; }

    public int? CountStudents { get; set; }

    public int? CountPedControl { get; set; }

    public int? CountWorkOfStud { get; set; }

    public int? CountWorkOfParents { get; set; }

    public int? CountOrphans { get; set; }

    public int? CountPdnstud { get; set; }

    public int? CountOvzstud { get; set; }

     private int? _countHostelVisits;
    public int? CountHostelVisits { 
        get{
            return _countHostelVisits;
        }
    set
    {
   _countHostelVisits = Group.Students.Where(i=>i.GraphicVisitsHostels.Count!=0).Sum(i=>i.GraphicVisitsHostels.Where(p=>p.Semestr==Semestr).Count());
    } }

    public int? CountCommunHours { get; set; }

    public int? CountParentsMeetings { get; set; }

    public int? CountStudInEvents { get; set; }

    public DateTime Date { get; set; }

    public virtual Group Group { get; set; } = null!;

}
