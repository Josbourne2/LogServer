using Microsoft.Extensions.Logging;
using Serilog.Core;
using Serilog.Sinks.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LogServer.Core
{
    public class CustomHttpClient : IHttpClient
    {
        private readonly HttpClient httpClient;

        public CustomHttpClient()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", "secret-api-key");
        }

        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {            
            return httpClient.PostAsync(requestUri, content);
        }

        public void Dispose() => httpClient?.Dispose();
    }
}