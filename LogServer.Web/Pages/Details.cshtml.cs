using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LogServer.Core;
using LogServer.Data;

namespace LogServer.Web.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly LogServer.Data.LogServerDbContext _context;

        public DetailsModel(LogServer.Data.LogServerDbContext context)
        {
            _context = context;
        }

        public LogEvent LogEvent { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LogEvent = await _context.LogEvents.FirstOrDefaultAsync(m => m.Id == id);

            if (LogEvent == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
