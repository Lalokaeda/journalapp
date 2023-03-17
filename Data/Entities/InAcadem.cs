using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace journalapp.Data.Entities
{
    public class InAcadem
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int Semestr { get; set; }

        public Student Student { get; set; } = null!;
    }
}