using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace journalapp;

public partial class Business
{
    public int Id { get; set; }

  //  public DateTime Year { get; set; }

     public int Semester { get; set; }

    [Required(ErrorMessage = "Выберите студента")]
    [Display(Name = "Студент")]
    public int StudentId { get; set; }

    [Display(Name = "Секция")]
    public string? Workshop { get; set; }

    [Display(Name = "Студенческая ассоциация")]
    public int? StudentAssotiationId { get; set; }
    

    public virtual Student Student { get; set; } = null!;

    public virtual StudentAssotiation? StudentAssotiation { get; set; }
}
