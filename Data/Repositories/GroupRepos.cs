using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace journalapp.Data.Repositories
{
    public class GroupRepos
    {
        private readonly JournalContext context;
        public GroupRepos(JournalContext context)
        {
            this.context = context;
        }

        public IQueryable<Group> GetGroups()
        {
            return context.Groups;
        }

        public Group GetGroupsById(string id)
        {
            return context.Groups.FirstOrDefault(x => x.Id == id);
        }

        public void SaveGroups(Group entity)
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteGroups(string id)
        {
            context.Groups.Remove(new Group() { Id = id });
            context.SaveChanges();
        }
    }
}