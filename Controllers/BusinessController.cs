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
//    [Route("[controller]")]
    public class BusinessController : Controller
    {
       private readonly JournalContext _DBcontext;

        public BusinessController(JournalContext context)
        {
            _DBcontext = context;
        }

        public IActionResult BusinessList()
        {
            List<Business> businessList=_DBcontext.Businesses.Where(i=>i.Student.Expelleds.Count==0&&i.Student.InAcadems.Count==0)
                                                                .Include(i=>i.Student).Include(i=>i.StudentAssotiation)
                                                                .OrderBy(i=>i.Student.Surname).AsNoTracking().ToList();
            return View(businessList);
        }

        public IActionResult AddEdit(int? Id)
        {   
            BusinessViewModel currentBusiness=new BusinessViewModel{
                StudentsList=_DBcontext.Students.Where(i=>i.Expelleds.Count==0&&i.InAcadems.Count==0)
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
                currentBusiness.Business=_DBcontext.Businesses.Where(i=>i.Id==Id).FirstOrDefault();
                
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