using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LogServer.Core
{
    public class LogEvent
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }

        public string Level { get; set; }
        public string RenderedMessage { get; set; }
        public string MessageTemplate { get; set; }
        public string Properties { get; set; }
        public string Exception { get; set; }
        public Guid CorrelationId { get; set; }
    }
}
