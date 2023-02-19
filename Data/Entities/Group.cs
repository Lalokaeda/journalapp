using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace journalapp;

public partial class Group
{ 
     public string Id { get; set; } = null!;

    public string SpecialityId { get; set; } = null!;

    public DateTime RecruitmentYear { get; set; }

    public string EmpId { get; set; }

    public virtual Emp Emp { get; set; } = null!;

    public virtual ICollection<CommunicationHour> CommunicationHours { get; } = new List<CommunicationHour>();

    public virtual ICollection<CourseOfGroup> CourseOfGroups { get; } = new List<CourseOfGroup>();

    public virtual ICollection<Curator> Curators { get; } = new List<Curator>();

    public virtual ICollection<ParentMeeting> ParentMeetings { get; } = new List<ParentMeeting>();

    public virtual ICollection<Passport> Passports { get; } = new List<Passport>();

    public virtual Speciality Speciality { get; set; } = null!;

    public virtual ICollection<Student> Students { get; } = new List<Student>();
}
