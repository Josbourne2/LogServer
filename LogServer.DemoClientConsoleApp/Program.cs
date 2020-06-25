using System;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace LogServer.DemoClientConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:5001/");
            Guid correlationId = Guid.NewGuid();

            request.Headers.Add("X-Correlation-ID", correlationId.ToString());
            HttpResponseMessage response = client.SendAsync(request).GetAwaiter().GetResult();
            Console.WriteLine($"Webpagina opgevraagd met cor relatio n id: {correlationId}");
        }
    }
}