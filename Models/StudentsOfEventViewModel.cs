using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace journalapp.Models
{
    public class StudentsOfEventViewModel
    {
        public StudentsOfEvent StudentsOfEvent { get; set; }

        public List<SelectListItem>? StudentsList { get; set; }
        public List<SelectListItem> EventsList { get; set; }
    }
}