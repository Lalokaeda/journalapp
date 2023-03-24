using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly RoleManager<IdentityRole> _roleManager;

        public EmpController(JournalContext context)
        {
            _DBcontext = context;
        }

        public async Task<IActionResult> EmpList()
        {
            List<Emp> empList= await _DBcontext.Emps.ToListAsync();
            return View(empList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}