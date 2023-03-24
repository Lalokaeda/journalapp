using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using journalapp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace journalapp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmpController : Controller
    {
        private readonly JournalContext _DBcontext;
        private readonly UserManager<Emp> _userManager;

        public EmpController(JournalContext context, UserManager<Emp> userManager)
        {
            _DBcontext = context;
            _userManager=userManager;
        }

        public async Task<IActionResult> EmpList()
        {
            List<Emp> empList= await _DBcontext.Emps.Include(i=>i.Groups).ToListAsync();
            List<EmpViewModel> empViewModels = new List<EmpViewModel>();
            foreach(var emp in empList)
            {
                EmpViewModel empVM = new EmpViewModel{
                    Emp=emp,
                    Roles= (List<string>)await _userManager.GetRolesAsync(emp)
                };
                //empVM.
                empViewModels.Add(empVM);
                empViewModels=empViewModels.OrderBy(i=>i.Emp.Surname).ToList();
            }
            
            return View(empViewModels);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}