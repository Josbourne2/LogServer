using LogServer.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogServer.API
{
    public static class Mapper
    {
        public static List<LogEvent> GetLogEventSet (LogEventForCreationDTO[] events)
        {
            return events.Select(@event => new LogEvent()
            {
                Timestamp = @event.Timestamp,
                Level = @event.Level,
                RenderedMessage = @event.RenderedMessage,
                MessageTemplate = @event.MessageTemplate,
                Properties = @event.Properties.ToString(),
                Exception = @event.Exception,
                CorrelationId=@event.CorrelationId
            }).ToList();
        }
    }
}
