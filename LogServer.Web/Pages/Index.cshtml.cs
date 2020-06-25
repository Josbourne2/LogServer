using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LogServer.Core;
using LogServer.Data;
using Serilog.Context;
using System.Diagnostics;

namespace LogServer.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly LogServer.Data.LogServerDbContext _context;

        public IndexModel(LogServer.Data.LogServerDbContext context)
        {
            _context = context;
        }

        public IList<LogEvent> LogEvent { get;set; }

        public async Task OnGetAsync()
        {
            

            

                LogEvent = await _context.LogEvents.ToListAsync();
            
        }
    }
}
