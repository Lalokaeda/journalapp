using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using journalapp.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace journalapp.Models
{
    public class SessionInf
    {
        public static string CurrentGroupId {get; set;}

        public static async Task SetCurrentGroupId(string group, HttpContext httpContext, string currentEmpId,
                                                JournalContext db, UserManager<Emp> userManager)
        {
            if(group!=null)
                httpContext.Session.Set(WC.currentGroup, group);
            else
            {
                List<Group> groups= await db.Groups.AsNoTracking().ToListAsync();
                if(httpContext.User != null)
                {
                    if(httpContext.User.IsInRole(WC.PrepodRole))
                        httpContext.Session.Set(WC.currentGroup, groups.Where(i=>i.EmpId==currentEmpId).FirstOrDefault().Id);
                    else
                        if(httpContext.User.IsInRole(WC.AdminRole))
                            httpContext.Session.Set(WC.currentGroup, groups.FirstOrDefault().Id);
                }
                else
                {
                    var currentEmp = await userManager.FindByIdAsync(currentEmpId);
                    if(groups.Where(i=>i.EmpId==currentEmp.Id).Count()>0 && await userManager.IsInRoleAsync(currentEmp, WC.PrepodRole))
                        httpContext.Session.Set(WC.currentGroup, groups.Where(i=>i.EmpId==currentEmp.Id));
                    if( await userManager.IsInRoleAsync(currentEmp, WC.AdminRole))
                        httpContext.Session.Set(WC.currentGroup, groups.FirstOrDefault().Id);

                }
            }    
            CurrentGroupId=httpContext.Session.Get<string>(WC.currentGroup);
        }
    }
}