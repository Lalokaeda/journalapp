using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace journalapp;

public partial class StudentsOfEvent
{
    public int Id { get; set; }
    
    public int Semestr { get; set; }
    [BindProperty, DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
    [Display(Name = "Дата и время проведения")]
    public DateTime? DatenTime { get; set; }

    [Required(ErrorMessage = "Выберите мероприятие")]
    [Display(Name = "Мероприятие")]
    public int EventId { get; set; }

    [Required(ErrorMessage = "Введите результат")]
    [Display(Name = "Результат")]
    public string Result { get; set; } = null!;

    [Required(ErrorMessage = "Выберите студента")]
    [Display(Name = "Студент")]
    public int StudentId { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
