using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LogServer.Core;
using LogServer.Data;

namespace LogServer.Web.Pages
{
    public class EditModel : PageModel
    {
        private readonly LogServer.Data.LogServerDbContext _context;

        public EditModel(LogServer.Data.LogServerDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(LogEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogEventExists(LogEvent.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LogEventExists(int id)
        {
            return _context.LogEvents.Any(e => e.Id == id);
        }
    }
}
