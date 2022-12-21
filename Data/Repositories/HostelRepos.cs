using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace journalapp.Data.Repositories
{
    public class HostelRepos
    {
         private readonly JournalContext context;
        public HostelRepos(JournalContext context)
        {
            this.context = context;
        }

        public IQueryable<Hostel> GetHostels()
        {
            return context.Hostels;
        }

        public Hostel GetHostelsById(int id)
        {
            return context.Hostels.FirstOrDefault(x => x.Id == id);
        }

        public void SaveHostels(Hostel entity)
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteHostels(int id)
        {
            context.Hostels.Remove(new Hostel() { Id = id });
            context.SaveChanges();
        }

    }
}