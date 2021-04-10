using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TPICAP.TechChallenge.Infrastructure.Models;

namespace TPICAP.TechChallenge.API.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(RequestDelegate next,
            ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.EnableBuffering();

            using (var reader = new StreamReader(
                context.Request.Body,
                Encoding.UTF8,
                false,
                1024,
                true))
            {
                var body = await reader.ReadToEndAsync();
                _logger.LogInformation(FormatRequest(body, context.Request));

                context.Request.Body.Position = 0;
            }

            await _next(context);
        }

        private string FormatRequest(string body, HttpRequest request)
        {
            var requestLog = new RequestLog
            {
                DateAndTime = DateTime.Now,
                RequestUrl = $"{request.Scheme} {request.Host}{request.Path} {request.QueryString}",
                PayloadData = body.Replace("\r\n", ""),
                CallerDetail = request.Headers["User-Agent"].ToString()
            };

            return JsonSerializer.Serialize(requestLog);
        }
    }
}