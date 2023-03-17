using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace journalapp.Data.Entities
{
    public class RemarksForLogging
    {
        public int Id { get; set; }
        public string GroupId { get; set; }=null!;

        public DateTime Date { get; set; }

        public string Remark { get; set; }=null!;

        public bool IsFixed { get; set; }

        public virtual Group Group { get; set; } = null!;
    }
}