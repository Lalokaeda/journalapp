using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using journalapp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace journalapp.Models
{
    public class StudentsOfRiskGroupViewModel
    {
        private int? _student;
        private IEnumerable<ReasonViewModel> _reasons;
        
        [Required(ErrorMessage = "Выберите студента")]
        [Display(Name = "Студент")]
        public Student Student
        {
            get; set;
        }
        public IEnumerable<ReasonViewModel> AllReasons{get; set;}
        public List<SelectListItem>? StudentsSelectList { get; set; }

    }
}