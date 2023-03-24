using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using journalapp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace journalapp.Controllers
{
    [Authorize]
    public class BusinessController : Controller
    {
        private readonly JournalContext _DBcontext;
        private readonly IHttpContextAccessor _accessor;
        private HttpContext _httpContext;
        private readonly UserManager<Emp> _userManager;
        private string currentEmpId;

        public BusinessController(JournalContext context, IHttpContextAccessor accessor, 
                                    UserManager<Emp> userManager)
        {
            _DBcontext=context;
            _accessor=accessor;
            _userManager=userManager;
            _httpContext=_accessor.HttpContext;
            currentEmpId= _userManager.GetUserId(_httpContext.User);
        }

        public async Task<IActionResult> BusinessList(string? group)
        {
            IEnumerable<SelectListItem> GroupsDropDown=_DBcontext.Groups.Where(i=>i.EmpId == currentEmpId).Select(i => new SelectListItem{
                Text=i.Id,
                Value=i.Id
            });
            ViewBag.GroupsDropDown=GroupsDropDown;
            if (group!=null | SessionInf.CurrentGroupId==null)
            SessionInf.SetCurrentGroupId(group, _httpContext, currentEmpId, _DBcontext, _userManager);
            List<Business> businessList= await _DBcontext.Businesses.Where(i=>i.Student.Expelleds.Count==0&&i.Student.InAcadems.Count==0 && i.Student.GroupId==SessionInf.CurrentGroupId)
                                                                .Include(i=>i.Student).Include(i=>i.StudentAssotiation)
                                                                .OrderBy(i=>i.Student.Surname).ToListAsync();
            return View(businessList);
        }

        public async Task<IActionResult> AddEdit(int? Id)
        {    
            if (SessionInf.CurrentGroupId==null)
            SessionInf.SetCurrentGroupId(null, _httpContext, currentEmpId, _DBcontext, _userManager);
            BusinessViewModel currentBusiness=new BusinessViewModel{
                StudentsList=_DBcontext.Students.Where(i=>i.Expelleds.Count==0&&i.InAcadems.Count==0 && i.GroupId==SessionInf.CurrentGroupId)
                                                .Select(i=> new SelectListItem{
                    Text=i.GetShortName(),
                    Value=i.Id.ToString()
                }).ToList(),
                AssotiationsList=_DBcontext.StudentAssotiations.Select(i=> new SelectListItem{
                    Text=i.Name,
                    Value=i.Id.ToString()
                }).ToList(),
            };

            if (Id==null)
            currentBusiness.Business=new Business();
            else {
                currentBusiness.Business=await _DBcontext.Businesses.Where(i=>i.Id==Id).FirstOrDefaultAsync();
                
            }
            return View(currentBusiness);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEdit(BusinessViewModel model)
        {
            if (model.Business.Id !=0)
            _DBcontext.Businesses.Update(model.Business);
            else{
                _DBcontext.Businesses.Add(model.Business);
            }
            _DBcontext.SaveChanges();
            return RedirectToAction("BusinessList");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}