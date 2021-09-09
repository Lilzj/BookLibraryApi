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
            //var request = await FormatRequest(context.Request);
            using var r = new StreamReader(context.Request.Body, Encoding.UTF8);
            var b = await r.ReadToEndAsync();
            Console.WriteLine(b);

            try
            {
                await _next(context);
            }
            finally
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
                    {
                        var body = await reader.ReadToEndAsync();

                        var logger = scope.ServiceProvider.GetRequiredService<LogService>();
                        logger.LogInfo(
                        $"Request {context.Request?.Method} Path:{context.Request?.Path.Value} => {b}"
                        );
                    };                    
                }
            }

            ////Copy a pointer to the original response body stream
            //var originalBodyStream = context.Response.Body;

            ////Create a new memory stream...
            //using (var responseBody = new MemoryStream())
            //{
            //    //...and use that for the temporary response body
            //    context.Response.Body = responseBody;

            //    //Continue down the Middleware pipeline, eventually returning to this class
            //    

            //    //Format the response from the server
            //    var response = await FormatResponse(context.Response);

            //    //TODO: Save log to chosen datastore

            //    //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
            //    await responseBody.CopyToAsync(originalBodyStream);
            //}
        }



        private async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;

            //This line allows us to set the reader for the request back at the beginning of its stream.
            request.EnableBuffering();

            //We now need to read the request stream.  First, we create a new byte[] with the same length as the request stream...
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            //...Then we copy the entire request stream into the new buffer.
            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            //We convert the byte[] into a string using UTF8 encoding...
            var bodyAsText = Encoding.UTF8.GetString(buffer);

            //..and finally, assign the read body back to the request body, which is allowed because of EnableRewind()
            //request.Body = body;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

    }
}
