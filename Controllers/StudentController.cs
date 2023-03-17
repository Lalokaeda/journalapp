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
using NuGet.Packaging;

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
            List<StudentViewModel> studentsVMList = new List<StudentViewModel>();
            List<Student> studentsList = _DBcontext.Students.Include(i=>i.Expelleds).Include(i=>i.InAcadems).AsNoTracking().ToList();
            foreach (var student in studentsList){
                StudentViewModel studentVM = new StudentViewModel{
                    Student=student
                    
                };
                studentsVMList.Add(studentVM);
            }
            studentsVMList=studentsVMList.OrderBy(i=>i.IsExpelled).ThenBy(i=>i.Student.Surname).ToList();
            return View(studentsVMList);
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
                StudentViewModel currentStudentVM = new StudentViewModel{
                    Student=_DBcontext.Students.Find(Id)
                };
                return View(currentStudentVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student newsStudent)
        {
            if (newsStudent.Id !=0)
            _DBcontext.Students.Update(newsStudent);
            else{
                if(newsStudent.RoomId!=0)
                newsStudent.Reasons.Add(_DBcontext.RiskGroups.Find(4));
                _DBcontext.Students.Add(newsStudent);
            }
            // if (newsStudent.IsExpelled)
            // newsStudent.Note+=" Отчислен";

            // if (newsStudent.IsAcadem)
            // newsStudent.Note+=" В академическом отпуске";
            _DBcontext.SaveChanges();
            return RedirectToAction("StudentList");
        }

       public IActionResult PositionsList()
        {
            List<Position> positions=_DBcontext.Positions.ToList();
            List<PositionsViewModel> positionsViewModels = new List<PositionsViewModel>();
            List<Student> students=_DBcontext.Students.ToList();
            foreach(var obj in positions)
            {
                PositionsViewModel posVM= new PositionsViewModel{
            Position=obj
            // StudentsSelectList=students.Where(i=>i.Patronymic!=null).Select(i => new SelectListItem{
            //     Text= i.GetFullName(),
            //     Value=i.Id.ToString()
            //     }).ToList()
            };
            try{
                 posVM.CurStudent=students.Where(i=> i.Position==obj).FirstOrDefault();
            } 
            catch{}
            positionsViewModels.Add(posVM);
            }
            return View(positionsViewModels);
        }

        public IActionResult AddStudentOnPosition(int Id)
        {
             Position selectedPosition=_DBcontext.Positions.Find(Id);
            List<Student> studentsWithoutPosition=_DBcontext.Students.ToList();
                PositionsViewModel posVM= new PositionsViewModel{
            Position=selectedPosition,
            StudentsSelectList=studentsWithoutPosition.Select(i => new SelectListItem{
                Text= i.GetFullName(),
                Value=i.Id.ToString()
                }).ToList()
            };
            try{
                posVM.CurStudent=_DBcontext.Students.Where(i=> i.PositionId==Id).Include(i=>i.Position).FirstOrDefault();
            }
            catch{
            }
            return View(posVM);
        }

         [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddStudentOnPosition(PositionsViewModel newsStudentPosition)
        {  
            Student currentStudent=_DBcontext.Students.Find(newsStudentPosition.CurStudent.Id);
            currentStudent.PositionId= newsStudentPosition.CurStudent.PositionId;
            _DBcontext.Students.Update(currentStudent);
            _DBcontext.SaveChanges();
            return RedirectToAction("PositionsList");
        }

        public IActionResult StudHealthGroups(){
        IEnumerable<Student> studentsList=_DBcontext.Students.Include(i=>i.HealthGroup);
            return View(studentsList);
        }

  public IActionResult EditHealthGroup(int Id)
        { 
            IEnumerable<SelectListItem> HealthGroupsDropDown=_DBcontext.HealthGroups.Select(i => new SelectListItem{
                Text=i.Name,
                Value=i.Id.ToString()
            });

            ViewBag.HealthGroupsDropDown=HealthGroupsDropDown;
                Student currentStudent=_DBcontext.Students.Find(Id);
                return View(currentStudent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditHealthGroup(Student newsStudentsHealthGroup)
        {
            _DBcontext.Students.Update(newsStudentsHealthGroup);
            _DBcontext.SaveChanges();
            return RedirectToAction("StudHealthGroups");
        }

        public IActionResult StudOfRiskGroup(){
        IEnumerable<Student> studentsList=_DBcontext.Students.Where(i=>i.Reasons.Count!=0)
                                            .Include(i=>i.Reasons);
            return View(studentsList);
        }

         public IActionResult AddEditRiskGroups(int? Id)
        {
            List<Student> students=_DBcontext.Students.Include(i=>i.Reasons).ToList();
            StudentsOfRiskGroupViewModel StudRiskVM;
            if (Id !=null)
            {
                Student selectedStudent=students.Where(i=>i.Id==Id).FirstOrDefault();
                StudRiskVM= new StudentsOfRiskGroupViewModel{
                    Student=Id,
                    StudentsSelectList=students.Select(i => new SelectListItem{
                        Text= i.GetFullName(),
                        Value=i.Id.ToString()
                    }).ToList(),
                    AllReasons=_DBcontext.RiskGroups.Select(i=> new ReasonViewModel(){
                        Reason=i,
                        Selected = selectedStudent.Reasons.Contains(i)? true:false
                    })};
                   
            }
            else{
                StudRiskVM= new StudentsOfRiskGroupViewModel{
                    StudentsSelectList=students.Select(i => new SelectListItem{
                        Text= i.GetFullName(),
                        Value=i.Id.ToString()
                    }).ToList(),
                    AllReasons=_DBcontext.RiskGroups.Select(i=> new ReasonViewModel{
                        Reason=i,
                        Selected=false
                    })};
            }
 return View(StudRiskVM);
            }
            
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEditRiskGroups(StudentsOfRiskGroupViewModel model)
        {
            Student currentStudent=_DBcontext.Students.Include(i=>i.Reasons).Where(i=>i.Id==model.Student).FirstOrDefault();
            foreach(var reason in model.AllReasons)
            {
               if(reason.Selected && !reason.Reason.Students.Contains(currentStudent))
                reason.Reason.Students.Add(currentStudent);
                if(!reason.Selected && reason.Reason.Students.Contains(currentStudent))
                reason.Reason.Students.Remove(currentStudent);
                try{
                     _DBcontext.Entry(reason.Reason).State = EntityState.Modified;
                } catch{}
            }
             _DBcontext.SaveChanges();
            //_DBcontext.Students.Update(currentStudent);
            
            
            return RedirectToAction("StudOfRiskGroup");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

    }
}