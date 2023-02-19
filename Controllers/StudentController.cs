using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using journalapp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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


        public IActionResult StudentList()
        {
            List<Student> studentsList=_DBcontext.Students.ToList();
            return View(studentsList);
        }

        public IActionResult Create(int? Id)
        { 
            IEnumerable<SelectListItem> GroupsDropDown=_DBcontext.Groups.Select(i => new SelectListItem{
                Text=i.Id,
                Value=i.Id
            });
            IEnumerable<SelectListItem> RoomsDropDown=_DBcontext.Rooms.Select(i => new SelectListItem{
                Text= i.Hostel.Address+", комната №"+i.NumofRoom.ToString(),
                Value=i.Id.ToString()
            });

            ViewBag.GroupsDropDown=GroupsDropDown;
            ViewBag.RoomsDropDown=RoomsDropDown;
            if (Id==0)
            return View();
            else {
                Student currentStudent=_DBcontext.Students.Where(i=>i.Id==Id).FirstOrDefault();
                return View(currentStudent);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student newsStudent)
        {
            if (newsStudent.Id !=0)
            _DBcontext.Students.Update(newsStudent);
            else
            _DBcontext.Students.Add(newsStudent);
            _DBcontext.SaveChanges();
            return RedirectToAction("StudentList");
        }

       public IActionResult PositionsList()
        {
          List<Student> studsWithPos = _DBcontext.Students.Where(i=>i.PositionId!=0).ToList();
          return View(studsWithPos);
        }

        public IActionResult AddStudentOnPosition()
        {
             List<Position> positions=_DBcontext.Positions.ToList();
            List<PositionsViewModel> positionsViewModels = new List<PositionsViewModel>();
            List<Student> studentsWithoutPosition=_DBcontext.Students.Where(i=>i.PositionId==0).ToList();
            foreach(var obj in positions)
            {
                PositionsViewModel posVM= new PositionsViewModel{
            Position=obj,
            StudentsSelectList=studentsWithoutPosition.Where(i=>i.Patronymic!=null).Select(i => new SelectListItem{
                Text= i.Surname+" "+i.Name+" " + i.Patronymic,
                Value=i.Id.ToString()
                }).ToList()
            };
             try
                {
                  posVM.StudentsSelectList.AddRange(studentsWithoutPosition.Where(i=>i.Patronymic==null).Select(i => new SelectListItem{
                Text= i.Surname+" "+i.Name,
                Value=i.Id.ToString()
                }).ToList());
                }
                catch (System.Exception)
                {
                    
                    throw;
                }
            positionsViewModels.Add(posVM);
            }
            return View(positionsViewModels);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}