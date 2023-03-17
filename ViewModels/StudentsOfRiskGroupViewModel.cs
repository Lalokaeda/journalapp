using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace journalapp.ViewModels
{
    public class StudentsOfRiskGroupViewModel
    {
        private int? _student;
        private IEnumerable<ReasonViewModel> _reasons;
        public int? Student
        {
            get;
            set;
        }
        public IEnumerable<ReasonViewModel> AllReasons{get; set;}
        public List<SelectListItem>? StudentsSelectList { get; set; }

    }
}