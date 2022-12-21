using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace journalapp;

public partial class AspNetUser : IdentityUser
{

    public int? ClassTeacherId { get; set; }
    public virtual ClassTeacher? ClassTeacher { get; set; }

}
