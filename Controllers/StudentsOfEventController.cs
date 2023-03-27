using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using journalapp.Models;
using Microsoft.AspNetCore.Identity;
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
        private readonly IHttpContextAccessor _accessor;
        private HttpContext _httpContext;
        private readonly UserManager<Emp> _userManager;
        private string currentEmpId;
        List<SelectListItem> GroupsDropDown=new List<SelectListItem>();
        public StudentsOfEventController(JournalContext context, IHttpContextAccessor accessor, 
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

          public async Task<IActionResult> StudOfEventList(string? group)
        {
            ViewBag.GroupsDropDown=GroupsDropDown;
            if (group!=null || SessionInf.CurrentGroupId==null)
                await SessionInf.SetCurrentGroupId(group, _httpContext, currentEmpId, _DBcontext, _userManager);

            List<StudentsOfEvent> studOfEventList= await _DBcontext.StudentsOfEvents.Where(i=>i.Student.Expelleds.Count==0&&i.Student.InAcadems.Count==0 && i.Student.GroupId==SessionInf.CurrentGroupId)
                                                                            .Include(i=>i.Student).Include(i=>i.Event)
                                                                            .OrderBy(i=>i.Student.Surname).ToListAsync();
            return View(studOfEventList);
        }

        public async Task<IActionResult> AddEdit(int? Id)
        {   
            StudentsOfEventViewModel currentStudOfEvent=new StudentsOfEventViewModel{
                StudentsList= await _DBcontext.Students.Where(i=>i.Expelleds.Count==0&&i.InAcadems.Count==0 
                                                                && i.GroupId==SessionInf.CurrentGroupId)
                                                                .Select(i=> new SelectListItem{
                    Text=i.GetShortName(),
                    Value=i.Id.ToString()
                }).ToListAsync(),
                EventsList= await _DBcontext.Events.Select(i=> new SelectListItem{
                    Text=i.Name,
                    Value=i.Id.ToString()
                }).ToListAsync(),
            };

            if (Id==null || Id==0)
            currentStudOfEvent.StudentsOfEvent=new StudentsOfEvent();
            else
                currentStudOfEvent.StudentsOfEvent= await _DBcontext.StudentsOfEvents.Where(i=>i.Id==Id).FirstOrDefaultAsync();
                
            return View(currentStudOfEvent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEdit(StudentsOfEventViewModel model)
        {
            if (model.StudentsOfEvent.Id !=0 && model.StudentsOfEvent.Id!=null)
            _DBcontext.StudentsOfEvents.Update(model.StudentsOfEvent);
            else{
                _DBcontext.StudentsOfEvents.Add(model.StudentsOfEvent);
            }
            await _DBcontext.SaveChangesAsync();
            return RedirectToAction("StudOfEventList");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}