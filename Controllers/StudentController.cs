using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using journalapp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace journalapp.Controllers
{
 //   [Route("[controller]")]
    public class StudentController : Controller
    {
        private readonly JournalContext _DBcontext;
        public StudentController(JournalContext context)
        {
            _DBcontext=context;
        }


        public IActionResult Edit(int Id)
        {
            var entity = Datamanager.student.GetStudentsById(Id);
            return View(entity);
        }

        [HttpPost]
        public IActionResult Edit(Student model)
        {
            if (ModelState.IsValid)
            {
                Datamanager.student.SaveStudents(model);
                return RedirectToAction();
            }
            return View(model);
        }

        public IActionResult StudentList()
        {
            List<Student> studentsList=_DBcontext.Students.ToList();
            return View(studentsList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}