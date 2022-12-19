using System;
using System.Collections.Generic;

namespace journalapp;

public partial class Group
{
    public string Id { get; set; } = null!;

    public string SpecialityId { get; set; } = null!;

    public DateTime RecruitmentYear { get; set; }

    public string ClassTeacherId { get; set; }

    public virtual ClassTeacher ClassTeacher { get; set; } = null!;

    public virtual ICollection<CourseOfGroup> CourseOfGroups { get; } = new List<CourseOfGroup>();

    public virtual ICollection<Curator> Curators { get; } = new List<Curator>();

    public virtual Speciality Speciality { get; set; } = null!;

    public virtual ICollection<Student> Students { get; } = new List<Student>();
}
