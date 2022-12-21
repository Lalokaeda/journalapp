using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace journalapp.Data.Repositories
{
    public class ClassTeacherRepos
    {
        private readonly JournalContext context;
        public ClassTeacherRepos(JournalContext context)
        {
            this.context = context;
        }

        public IQueryable<ClassTeacher> GetClassTeachers()
        {
            return context.ClassTeachers;
        }

        public ClassTeacher GetClassTeachersById(int id)
        {
            return context.ClassTeachers.FirstOrDefault(x => x.Id == id);
        }

        public void SaveClassTeachers(ClassTeacher entity)
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteClassTeachers(int id)
        {
            context.ClassTeachers.Remove(new ClassTeacher() { Id = id });
            context.SaveChanges();
        }
    }
}