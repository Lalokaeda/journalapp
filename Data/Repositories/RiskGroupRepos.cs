using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace journalapp.Data.Repositories
{
    public class RiskGroupRepos
    {
        private readonly JournalContext context;
        public RiskGroupRepos(JournalContext context)
        {
            this.context = context;
        }

        public IQueryable<RiskGroup> GetRiskGroups()
        {
            return context.RiskGroups;
        }

        public RiskGroup GetRiskGroupById(int id)
        {
            return context.RiskGroups.FirstOrDefault(x => x.Id == id);
        }

        public void SaveRiskGroups(RiskGroup entity)
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteRiskGroups(int id)
        {
            context.RiskGroups.Remove(new RiskGroup() { Id = id });
            context.SaveChanges();
        }
    }
}