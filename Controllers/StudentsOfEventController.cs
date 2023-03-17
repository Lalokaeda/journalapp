using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using journalapp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace journalapp.Controllers
{
  //  [Route("[controller]")]
    public class StudentsOfEventController : Controller
    {
        private readonly JournalContext _DBcontext;

        public StudentsOfEventController(JournalContext context)
        {
            _DBcontext = context;
        }

          public IActionResult StudOfEventList()
        {
            List<StudentsOfEvent> studOfEventList=_DBcontext.StudentsOfEvents.Where(i=>i.Student.Expelleds.Count==0&&i.Student.InAcadems.Count==0)
                                                                            .Include(i=>i.Student).Include(i=>i.Event)
                                                                            .OrderBy(i=>i.Student.Surname).ToList();
            return View(studOfEventList);
        }

        public IActionResult AddEdit(int? Id)
        {   
            StudentsOfEventViewModel currentStudOfEvent=new StudentsOfEventViewModel{
                StudentsList=_DBcontext.Students.Where(i=>i.Expelleds.Count==0&&i.InAcadems.Count==0)
                                                .Select(i=> new SelectListItem{
                    Text=i.GetShortName(),
                    Value=i.Id.ToString()
                }).ToList(),
                EventsList=_DBcontext.Events.Select(i=> new SelectListItem{
                    Text=i.Name,
                    Value=i.Id.ToString()
                }).ToList(),
            };

            if (Id==null)
            currentStudOfEvent.StudentsOfEvent=new StudentsOfEvent();
            else {
                currentStudOfEvent.StudentsOfEvent=_DBcontext.StudentsOfEvents.Where(i=>i.Id==Id).FirstOrDefault();
                
            }
            return View(currentStudOfEvent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEdit(StudentsOfEventViewModel model)
        {
            if (model.StudentsOfEvent.Id !=0)
            _DBcontext.StudentsOfEvents.Update(model.StudentsOfEvent);
            else{
                _DBcontext.StudentsOfEvents.Add(model.StudentsOfEvent);
            }
            _DBcontext.SaveChanges();
            return RedirectToAction("StudOfEventList");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}