using EBook_Library.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBook_Library.Midddlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public RequestLoggingMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            //First, get the incoming request
            using var r = new StreamReader(context.Request.Body, Encoding.UTF8);
            var b = await r.ReadToEndAsync();
            Console.WriteLine(b);

            try
            {
                await _next(context);
            }
            finally
            {
                var scope = _serviceProvider.CreateScope();
                
                    var reader = new StreamReader(context.Request.Body, Encoding.UTF8);
                    
                        var body = await reader.ReadToEndAsync();

                        var logger = scope.ServiceProvider.GetRequiredService<LogService>();
                        logger.LogInfo(
                        $"Request {context.Request?.Method} Path:{context.Request?.Path.Value} => {b}"
                        );
                                        
                
            }
        }



        private async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;

            //This line allows us to set the reader for the request back at the beginning of its stream.
            request.EnableBuffering();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body = body;
            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";

         
        }

    }
}
