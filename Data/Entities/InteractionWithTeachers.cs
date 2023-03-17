using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace journalapp.Data.Entities
{
    public class InteractionWithTeachers
    {
        public int Id {get; set;}

        public string EmpId{get; set;}=null!;

        public DateTime Date {get; set;}

        public int FormId {get; set;}

        public string Questions {get; set;}=null!;
        public string GroupId { get; set; }=null!;

        public Emp Emp { get; set; } = null!;

        public InteractionForm InteractionForm { get; set; } = null!;
        public Group Group { get; set; } = null!;

        
        
    }
}