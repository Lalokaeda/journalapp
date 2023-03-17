using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using journalapp.Data.Entities;

namespace journalapp;

public partial class Student
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Заполните фамилию")]
    [Display(Name = "Фамилия")]
    public string Surname { get; set; } = null!;

    [Required(ErrorMessage = "Заполните имя")]
    [Display(Name = "Имя")]
    public string Name { get; set; } = null!;

    [Display(Name = "Отчество")]
    public string? Patronymic { get; set; }

    [Required(ErrorMessage = "Выберите пол")]
    [Display(Name = "Пол")]
    public string Sex { get; set; } = null!;

    [Display(Name = "Группа здоровья")]
    public int? HealthGroupId { get; set; }

    [Required(ErrorMessage = "Заполните номер телефона")]
    [Display(Name = "Номер телефона")]
    public string PhoneNum { get; set; } = null!;

    [EmailAddress]
    [Display(Name = "Электронная почта")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Заполните адрес")]
    [Display(Name = "Адрес")]
    public string Address { get; set; } = null!;

    [Required(ErrorMessage = "Укажите группу")]
    [Display(Name = "Группа")]
    public string GroupId { get; set; } = null!;

    [Display(Name = "Коммерция")]
    public bool IsCommerce { get; set; }

 //   [Display(Name = "Отчислен")]
  //  public bool IsExpelled { get; set; }

  //  [Display(Name = "В академическом отпуске")]
 //   public bool IsAcadem { get; set; }

    [Display(Name = "Номер комнаты")]
    public int? RoomId { get; set; }
    
    [Display(Name = "Примечание")]
    public string? Note { get; set; }

    public int? PositionId { get; set; }
    public virtual ICollection<Business> Businesses { get; } = new List<Business>();

    public virtual ICollection<Curator> Curators { get; } = new List<Curator>();

    public virtual ICollection<GraphicVisitsHostel> GraphicVisitsHostels { get; } = new List<GraphicVisitsHostel>();

    public virtual Group Group { get; set; } = null!;

    public virtual HealthGroup? HealthGroup { get; set; }

    public virtual Room? Room { get; set; }

    public virtual Position? Position { get; set; }

    public virtual ICollection<StudentsOfEvent> StudentsOfEvents { get; } = new List<StudentsOfEvent>();

    public virtual ICollection<StudentsOnPedControl> StudentsOnPedControls { get; } = new List<StudentsOnPedControl>();

    public virtual ICollection<WorkWithStudent> WorkWithStudents { get; } = new List<WorkWithStudent>();

    public virtual ICollection<Parent> Parents { get; } = new List<Parent>();

    public virtual ICollection<RiskGroup> Reasons { get; set;} = new List<RiskGroup>();
    public virtual ICollection<IndividualAchiv> IndividualAchivs { get; } = new List<IndividualAchiv>();

    public virtual ICollection<Expelled> Expelleds { get; } = new List<Expelled>();

    public virtual ICollection<InAcadem> InAcadems { get; } = new List<InAcadem>();

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
    public string GetReasonsOfRiskGroup(){
         StringBuilder sb = new StringBuilder();
            foreach (var reason in Reasons)
            {
                sb.AppendLine(reason.Reason);
            }
        return sb.ToString();
    }
}
