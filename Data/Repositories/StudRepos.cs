using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace journalapp.Data.Repositories
{
    public class StudRepos
    {
         private readonly JournalContext context;
        public StudRepos(JournalContext context)
        {
            this.context = context;
        }

        public List<Student> GetStudents()
        {
            return context.Students.ToList();
        }

        public Student GetStudentsById(int id)
        {
            return context.Students.FirstOrDefault(x => x.Id == id);
        }

        public void SaveStudents(Student entity)
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteStudents(int id)
        {
            context.Students.Remove(new Student() { Id = id });
            context.SaveChanges();
        }


    }
}
