using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace journalapp.Models
{
    public class EmpViewModel
    {   
        private readonly UserManager<Emp> _userManager;

        public Emp Emp{ get; set;}
        public List<string> Roles {get; set;}
    }
}