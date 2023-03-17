using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace journalapp.Data.Entities
{
    public class InteractionForm
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<InteractionWithTeachers> InteractionWithTeachers { get; } = new List<InteractionWithTeachers>();
    }
}