using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace journalapp.Data.Repositories
{
    public class RoomRepos
    {
        private readonly JournalContext context;
        public RoomRepos(JournalContext context)
        {
            this.context = context;
        }

        public IQueryable<Room> GetRooms()
        {
            return context.Rooms;
        }

        public Room GetRoomsById(int id)
        {
            return context.Rooms.FirstOrDefault(x => x.Id == id);
        }

        public void SaveRooms(Room entity)
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