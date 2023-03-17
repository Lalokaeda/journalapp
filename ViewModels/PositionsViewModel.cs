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
        private Student _curStudent;
        public Student CurStudent {
            get
            {
                return _curStudent;
            } 
            set 
            {
            _curStudent=value;
            _curStudent.PositionId=this.Position.Id;
            }
        }
        public List<SelectListItem>? StudentsSelectList { get; set; }
    }
}