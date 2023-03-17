using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace journalapp.ViewModels
{
    public class StudentViewModel
    {
        public Student Student { get; set; }
        // private bool _isExpelled;
        public bool IsExpelled 
        { 
            get
            {
                return Student.Expelleds.Count>0? true:false;
            }
            // set
            // {
            //     _isExpelled = Student.Expelleds.Count>0? true:false;
            // }
        }
        // private bool _isInAcadem;
        public bool IsInAcadem
           { 
            get
            {
                return Student.InAcadems.Count()>0? true:false;
            }
            // set
            // {
            //     _isInAcadem = Student.InAcadems.Count()>0? true:false;
            // }
        }
    }
}