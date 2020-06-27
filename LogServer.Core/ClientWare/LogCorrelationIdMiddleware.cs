using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Threading.Tasks;

namespace LogServer.Core
{
    public static class LogCorrelationIdMiddlewareExtensions
    {
        public static IApplicationBuilder LogCorrelationId(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogCorrelationIdMiddleware>();
        }
    }

    public class LogCorrelationIdMiddleware
    {
        private const string header = "X-Correlation-ID";
        private readonly ILogger<LogCorrelationIdMiddleware> _logger;
        private readonly RequestDelegate _next;

        public LogCorrelationIdMiddleware(RequestDelegate next, ILogger<LogCorrelationIdMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Guid correlationId = GetOrSetCorrelationId(httpContext);

            try
            {
                using (LogContext.PushProperty("CorrelationId", correlationId))
                {
                    await _next(httpContext);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LogCorrelationIdMiddleware.");
                return;
            }
        }

        private Guid GetOrSetCorrelationId(HttpContext httpContext)
        {
            if (string.IsNullOrWhiteSpace(httpContext.Request.Headers[header]))
            {
                httpContext.Request.Headers[header] = Guid.NewGuid().ToString();
            }
            return Guid.Parse(httpContext.Request.Headers[header]);
        }
    }
}