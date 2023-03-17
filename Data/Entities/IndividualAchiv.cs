using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace journalapp.Data.Entities
{
    public class IndividualAchiv
    {
        public int Id { get; set; }

        public int Semester { get; set; }

    [Required(ErrorMessage = "Выберите студента")]
    [Display(Name = "Студент")]
        public int StudentId { get; set; }
    
    [Required(ErrorMessage = "Выберите направление деятельности")]
    [Display(Name = "Направление деятельности")]
        public int LOBId { get; set; }

        public string Achievement { get; set; }

        public virtual Student Student { get; set; } = null!;
        public virtual LineOfBusiness LOB { get; set; } = null!;

    }
}