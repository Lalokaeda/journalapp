using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace journalapp.Data.Repositories
{
    public class EventRepos
    {
        private readonly JournalContext context;
        public EventRepos(JournalContext context)
        {
            this.context = context;
        }

        public IQueryable<Event> GetEvents()
        {
            return context.Events;
        }

        public Event GetPEventsById(int id)
        {
            return context.Events.FirstOrDefault(x => x.Id == id);
        }

        public void SaveEvents(Event entity)
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteEvents(int id)
        {
            context.Events.Remove(new Event() { Id = id });
            context.SaveChanges();
        }
    }
}