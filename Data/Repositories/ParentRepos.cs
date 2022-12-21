using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace journalapp.Data.Repositories
{
    public class ParentRepos
    {
        private readonly JournalContext context;
        public ParentRepos(JournalContext context)
        {
            this.context = context;
        }

        public IQueryable<Parent> GetParents()
        {
            return context.Parents;
        }

        public Parent GetParentsById(int id)
        {
            return context.Parents.FirstOrDefault(x => x.Id == id);
        }

        public void SaveParents(Parent entity)
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteParents(int id)
        {
            context.Parents.Remove(new Parent() { Id = id });
            context.SaveChanges();
        }

    }
}