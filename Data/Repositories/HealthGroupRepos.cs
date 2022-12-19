using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace journalapp.Data.Repositories
{
    public class HealthGroupRepos
    {
                 private readonly JournalContext context;
        public HealthGroupRepos(JournalContext context)
        {
            this.context = context;
        }

        public IQueryable<HealthGroup> GetHealthGroups()
        {
            return context.HealthGroups;
        }

        public HealthGroup GetHealthGroupsById(int id)
        {
            return context.HealthGroups.FirstOrDefault(x => x.Id == id);
        }

        public void HealthGroups(HealthGroup entity)
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteHealthGroups(int id)
        {
            context.HealthGroups.Remove(new HealthGroup() { Id = id });
            context.SaveChanges();
        }


    }
}