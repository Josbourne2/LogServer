using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogServer.API
{
    public class LogEventForCreationDTO
    {
       
        public DateTime Timestamp { get; set; }
        public string Level { get; set; }
        public string RenderedMessage { get; set; }
        public string MessageTemplate { get; set; }
        public object Properties { get; set; }
        public string Exception { get; set; }

        public Guid CorrelationId { get; set; }
    }
}
