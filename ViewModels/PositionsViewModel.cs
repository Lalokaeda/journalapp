using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace journalapp.ViewModels
{
    public class PositionsViewModel
    {
        public Position Position { get; set; }
        public Student CurStudent {get; set;}
        public List<SelectListItem> StudentsSelectList { get; set; }
    }
}