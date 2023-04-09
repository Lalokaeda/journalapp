using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NuGet.Packaging;
using journalapp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using journalapp.Models;

namespace journalapp.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly JournalContext _DBcontext;
        private readonly IHttpContextAccessor _accessor;
        private HttpContext _httpContext;
        private readonly UserManager<Emp> _userManager;
        private string currentEmpId;
        List<SelectListItem> GroupsDropDown=new List<SelectListItem>();
        public StudentController(JournalContext context, IHttpContextAccessor accessor, 
                                    UserManager<Emp> userManager)
        {
            _DBcontext=context;
            _accessor=accessor;
            _userManager=userManager;
            _httpContext=_accessor.HttpContext;
            currentEmpId= _userManager.GetUserId(_httpContext.User);
            if(_httpContext.User.IsInRole(WC.AdminRole))
            GroupsDropDown= _DBcontext.Groups.Select(i => new SelectListItem{
                Text=i.Id,
                Value=i.Id
            }).ToList();
            if (_httpContext.User.IsInRole(WC.PrepodRole))
           GroupsDropDown= _DBcontext.Groups.Where(i=>i.EmpId==currentEmpId).Select(i => new SelectListItem{
                Text=i.Id,
                Value=i.Id
            }).ToList();
            
        }


        public async Task<IActionResult> StudentList(string? group)
        {   
            ViewBag.GroupsDropDown=GroupsDropDown;
            if (group!=null || SessionInf.CurrentGroupId==null)
                await SessionInf.SetCurrentGroupId(group, _httpContext, currentEmpId, _DBcontext, _userManager);

            List<StudentViewModel> studentsVMList = new List<StudentViewModel>();
            List<Student> studentsList = new List<Student>();
            studentsList= await _DBcontext.Students.Include(i=>i.Expelleds)
                                                    .Include(i=>i.InAcadems)
                                                    .Where(i=>i.GroupId==SessionInf.CurrentGroupId).ToListAsync();
            foreach (var student in studentsList){
                StudentViewModel studentVM = new StudentViewModel{
                    Student=student
                };
                studentsVMList.Add(studentVM);
            }
            studentsVMList=studentsVMList.OrderBy(i=>i.IsExpelled).ThenBy(i=>i.Student.Surname).ToList();

            return View(studentsVMList);
        }

        public async Task<IActionResult> Create(int? Id)
        { 
            ViewBag.GroupsDropDown=GroupsDropDown;
            IEnumerable<SelectListItem> RoomsDropDown=_DBcontext.Rooms.Where(i=>i.Students.Count<i.Roominess).Select(i => new SelectListItem{
                Text= i.Hostel.Address+", комната №"+i.NumofRoom.ToString(),
                Value=i.Id.ToString()
            });
            ViewBag.RoomsDropDown=RoomsDropDown;

            StudentViewModel currentStudentVM = new StudentViewModel();
            if (Id==0 || Id==null)
                currentStudentVM.Student = new Student{
                    GroupId=SessionInf.CurrentGroupId
                };
            else 
            {
                currentStudentVM.Student = await _DBcontext.Students.FindAsync(Id);
            }

            return View(currentStudentVM);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(StudentViewModel newsStudent)
        {
            var riskGroup= await _DBcontext.RiskGroups.Where(i=>i.Id==4).AsNoTracking().FirstOrDefaultAsync();
                
            if (newsStudent.IsExpelled)
                newsStudent.Student.Note+=" Отчислен";

            if (newsStudent.IsInAcadem)
                newsStudent.Student.Note+=" В академическом отпуске";

            if (newsStudent.Student.Id !=0 && newsStudent.Student.Id!=null)
                _DBcontext.Students.Update(newsStudent.Student);
            else
                 _DBcontext.Students.Add(newsStudent.Student);

            await _DBcontext.SaveChangesAsync();

             if (newsStudent.Student.Id !=0 && newsStudent.Student.Id !=null)
                {
                    var currentStudent = await _DBcontext.Students.Include(i=>i.Reasons).Where(i=> i.Id==newsStudent.Student.Id).FirstOrDefaultAsync();
                if(newsStudent.Student.RoomId!=0 && newsStudent.Student.RoomId!=null)
                {
                    if (currentStudent.Reasons.Where(i=>i.Id==riskGroup.Id).Count()==0)
                        currentStudent.Reasons.Add(riskGroup);
                }
                else
                    if (currentStudent.Reasons.Where(i=>i.Id==riskGroup.Id).Count()>0)
                        currentStudent.Reasons.Remove(riskGroup);
            _DBcontext.Students.Update(currentStudent);
            }

            await _DBcontext.SaveChangesAsync();

            return RedirectToAction("StudentList");
        }

       public async Task<IActionResult> PositionsList(string group)
        {   
            ViewBag.GroupsDropDown=GroupsDropDown;
            if (group!=null || SessionInf.CurrentGroupId==null)
                await SessionInf.SetCurrentGroupId(group, _httpContext, currentEmpId, _DBcontext, _userManager);
            List<Position> positions= await _DBcontext.Positions.ToListAsync();
            List<PositionsViewModel> positionsViewModels = new List<PositionsViewModel>();
            List<Student> students= await _DBcontext.Students.Where(i=>i.Expelleds.Count==0 && i.InAcadems.Count==0 
                                                                    && i.GroupId==SessionInf.CurrentGroupId).ToListAsync();

            foreach(var position in positions)
            {
                PositionsViewModel posVM= new PositionsViewModel{
                    Position=position
            // StudentsSelectList=students.Where(i=>i.Patronymic!=null).Select(i => new SelectListItem{
            //     Text= i.GetFullName(),
            //     Value=i.Id.ToString()
            //     }).ToList()
            };
            try
            {
                 posVM.CurStudent=students.Where(i=> i.Position==position).FirstOrDefault();
            } 
            catch{}
                positionsViewModels.Add(posVM);
            }

            return View(positionsViewModels);
        }

        public async Task<IActionResult> AddStudentOnPosition(int Id)
        {
            Position selectedPosition=await _DBcontext.Positions.FindAsync(Id);
            List<Student> students = await _DBcontext.Students.Where(i=>i.Expelleds.Count==0&&i.InAcadems.Count==0 
                                                                            && i.GroupId ==SessionInf.CurrentGroupId).ToListAsync();
            
            PositionsViewModel posVM= new PositionsViewModel{
                Position=selectedPosition,
                StudentsSelectList= students.Select(i => new SelectListItem{
                    Text= i.GetFullName(),
                    Value=i.Id.ToString()
                }).ToList()
            };
            try
            {
                posVM.CurStudent= await _DBcontext.Students.Where(i=> i.PositionId==Id).Include(i=>i.Position).FirstOrDefaultAsync();
            }
            catch{}

            return View(posVM);
        }

         [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStudentOnPosition(PositionsViewModel newsStudentPosition)
        {  
            Student currentStudent;
            try{
            currentStudent= await _DBcontext.Students.Where(i=>i.PositionId== newsStudentPosition.CurStudent.PositionId
                                    && i.GroupId == SessionInf.CurrentGroupId).FirstOrDefaultAsync();
            currentStudent.PositionId= null;
            _DBcontext.Students.Update(currentStudent);
            } catch{}
            currentStudent= await _DBcontext.Students.FindAsync(newsStudentPosition.CurStudent.Id);
            currentStudent.PositionId= newsStudentPosition.CurStudent.PositionId;
            _DBcontext.Students.Update(currentStudent);
            _DBcontext.SaveChanges();
            
            return RedirectToAction("PositionsList");
        }

        public async Task<IActionResult> StudHealthGroups(string? group)
        {
            ViewBag.GroupsDropDown=GroupsDropDown;
            if (group!=null | SessionInf.CurrentGroupId==null)
                await SessionInf.SetCurrentGroupId(group, _httpContext, currentEmpId, _DBcontext, _userManager);
            IEnumerable<Student> studentsList= await _DBcontext.Students.Include(i=>i.HealthGroup)
                                                                        .Where(i=>i.Expelleds.Count==0&&i.InAcadems.Count==0
                                                                                && i.GroupId==SessionInf.CurrentGroupId)
                                                                                .AsNoTracking().ToListAsync();
           
            return View(studentsList);
        }

        public async Task<IActionResult> EditHealthGroup(int Id)
        { 
            IEnumerable<SelectListItem> HealthGroupsDropDown=_DBcontext.HealthGroups.Select(i => new SelectListItem{
                Text=i.Name,
                Value=i.Id.ToString()
            });
            ViewBag.HealthGroupsDropDown=HealthGroupsDropDown;

            Student currentStudent= await _DBcontext.Students.FindAsync(Id);
                
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

        public async Task<IActionResult> StudOfRiskGroup(string? group)
        {
            ViewBag.GroupsDropDown=GroupsDropDown;

            if (group!=null | SessionInf.CurrentGroupId==null)
                await SessionInf.SetCurrentGroupId(group, _httpContext, currentEmpId, _DBcontext, _userManager);
            IEnumerable<Student> studentsList= await _DBcontext.Students.Where(i=>i.Reasons.Count!=0 && i.Expelleds.Count==0
                                                                                 && i.InAcadems.Count==0 && i.GroupId==SessionInf.CurrentGroupId).Include(i=>i.Reasons).ToListAsync();
            
            return View(studentsList);
        }

         public async Task<IActionResult> AddEditRiskGroups(int? Id)
        {
            List<Student> students= await _DBcontext.Students.Include(i=>i.Reasons)
                                                            .Where(i=>i.Expelleds.Count==0
                                                                 && i.InAcadems.Count==0 && i.GroupId==SessionInf.CurrentGroupId).ToListAsync();
            StudentsOfRiskGroupViewModel StudRiskVM;
            
            if (Id !=null)
            {
                Student selectedStudent=students.Where(i=>i.Id==Id).FirstOrDefault();
                StudRiskVM= new StudentsOfRiskGroupViewModel{
                    Student=selectedStudent,
                    AllReasons=_DBcontext.RiskGroups.Select(i=> new ReasonViewModel(){
                        Reason=i,
                        Selected = selectedStudent.Reasons.Contains(i)? true:false
                    })
                };   
                 
            }
            else
            {
                StudRiskVM= new StudentsOfRiskGroupViewModel{
                    StudentsSelectList=students.Where(i=>i.Reasons.Count==0).Select(i => new SelectListItem{
                        Text= i.GetFullName(),
                        Value=i.Id.ToString()
                    }).ToList(),
                    AllReasons=_DBcontext.RiskGroups.Select(i=> new ReasonViewModel{
                        Reason=i,
                        Selected=false
                    }),
                    Student = new Student()
                };
            }

            return View(StudRiskVM);
        }
            
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEditRiskGroups(StudentsOfRiskGroupViewModel model)
        {
            // if(model.AllReasons.Where(i=>i.Selected==true).Count()==0)
            //     return RedirectToAction("StudOfRiskGroup");

            Student currentStudent= await _DBcontext.Students.Include(i=>i.Reasons).Where(i=>i.Id==model.Student.Id).FirstOrDefaultAsync();
            foreach(var reason in model.AllReasons)
            {
               if(reason.Selected && currentStudent.Reasons.Where(i=>i.Id==reason.Reason.Id).Count()==0)
                currentStudent.Reasons.Add(reason.Reason);
                if(!reason.Selected && currentStudent.Reasons.Where(i=>i.Id==reason.Reason.Id).Count()>0){
                    currentStudent.Reasons.Remove(currentStudent.Reasons.Where(i=>i.Id==reason.Reason.Id).First());
                    if (reason.Reason.Id==4)
                        currentStudent.RoomId=null;
                }
                
                     
            }
            _DBcontext.Students.Update(currentStudent);
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