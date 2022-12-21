using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace journalapp.Views.Pages
{
    public class ListGroup : PageModel
    {
        private readonly ILogger<ListGroup> _logger;

        public ListGroup(ILogger<ListGroup> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}