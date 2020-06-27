using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogServer.Core;
using LogServer.Data;

namespace LogServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogEventsController : ControllerBase
    {
        private readonly LogServerDbContext _context;

        public LogEventsController(LogServerDbContext context)
        {
            _context = context;
        }

        // GET: api/LogEvents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogEvent>>> GetLogEvents()
        {
            return await _context.LogEvents.ToListAsync();
        }

        // GET: api/LogEvents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LogEvent>> GetLogEvent(int id)
        {
            var logEvent = await _context.LogEvents.FindAsync(id);

            if (logEvent == null)
            {
                return NotFound();
            }

            return logEvent;
        }

        // PUT: api/LogEvents/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogEvent(int id, LogEvent logEvent)
        {
            if (id != logEvent.Id)
            {
                return BadRequest();
            }

            _context.Entry(logEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogEventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LogEvents
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task PostLogEvent(int? inkomstenverstrekkerId, [FromBody] object logEvents)
        //{
        //    Console.WriteLine(logEvents.ToString());
        //    //var logs = Mapper.GetLogEventSet(logEvents.Events);
        //    //_context.LogEvents.AddRange(logs);
        //    //await _context.SaveChangesAsync();
        //}

        // POST: api/LogEvents
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task PostLogEvent(int? inkomstenverstrekkerId, [FromBody] LogEventDtoArray logEvents)
        {
            Console.WriteLine(logEvents.ToString());
            var logs = Mapper.GetLogEventSet(logEvents.Events);
            _context.LogEvents.AddRange(logs);
            await _context.SaveChangesAsync();
        }

        // DELETE: api/LogEvents/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LogEvent>> DeleteLogEvent(int id)
        {
            var logEvent = await _context.LogEvents.FindAsync(id);
            if (logEvent == null)
            {
                return NotFound();
            }

            _context.LogEvents.Remove(logEvent);
            await _context.SaveChangesAsync();

            return logEvent;
        }

        private bool LogEventExists(int id)
        {
            return _context.LogEvents.Any(e => e.Id == id);
        }
    }
}