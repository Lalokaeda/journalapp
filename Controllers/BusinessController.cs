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
        List<SelectListItem> GroupsDropDown=new List<SelectListItem>();

        public BusinessController(JournalContext context, IHttpContextAccessor accessor, 
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

        public async Task<IActionResult> BusinessList(string? group)
        {
            ViewBag.GroupsDropDown=GroupsDropDown;
            if (group!=null || SessionInf.CurrentGroupId==null)
            await SessionInf.SetCurrentGroupId(group, _httpContext, currentEmpId, _DBcontext, _userManager);

            List<Business> businessList= await _DBcontext.Businesses.Include(i=>i.Student).Include(i=>i.StudentAssotiation)
                                                                        .Where(i=>i.Student.Expelleds.Count==0&&i.Student.InAcadems.Count==0 && i.Student.GroupId==SessionInf.CurrentGroupId)
                                                                        .OrderBy(i=>i.Student.Surname).ToListAsync();
            return View(businessList);
        }

        public async Task<IActionResult> AddEdit(int? Id)
        {    
            if (SessionInf.CurrentGroupId==null)
                await SessionInf.SetCurrentGroupId(null, _httpContext, currentEmpId, _DBcontext, _userManager);

            BusinessViewModel currentBusiness=new BusinessViewModel{
                StudentsList=await _DBcontext.Students.Where(i=>i.Expelleds.Count==0&&i.InAcadems.Count==0 && i.GroupId==SessionInf.CurrentGroupId)
                                                .Select(i=> new SelectListItem{
                    Text=i.GetShortName(),
                    Value=i.Id.ToString()
                }).ToListAsync(),
                AssotiationsList= await _DBcontext.StudentAssotiations.Select(i=> new SelectListItem{
                    Text=i.Name,
                    Value=i.Id.ToString()
                }).ToListAsync(),
            };

            if (Id==null || Id==0)
                currentBusiness.Business=new Business();
            else 
                currentBusiness.Business=await _DBcontext.Businesses.Where(i=>i.Id==Id).FirstOrDefaultAsync(); 

            return View(currentBusiness);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEdit(BusinessViewModel model)
        {   if((model.Business.StudentAssotiationId == null ||model.Business.StudentAssotiationId == 0)
                && (model.Business.Workshop==null || model.Business.Workshop.Trim()==""))
                return RedirectToAction("BusinessList");
                
            if (model.Business.Id !=0 && model.Business.Id!=null)
            _DBcontext.Businesses.Update(model.Business);
            else{
                _DBcontext.Businesses.Add(model.Business);
            }
            await _DBcontext.SaveChangesAsync();
            return RedirectToAction("BusinessList");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}