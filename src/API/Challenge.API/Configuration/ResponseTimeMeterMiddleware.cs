using Serilog;
using System.Diagnostics;

namespace Challenge.API.Configuration
{
    public class ResponseTimeMeterMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Serilog.ILogger logger;

        public ResponseTimeMeterMiddleware(RequestDelegate next, Serilog.ILogger logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                watch.Stop();

                var responseTime = watch.ElapsedMilliseconds;
                var statusCode = context.Response.StatusCode;

                responseBody.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);

                logger.Information($"Request {context.Request.Method} {context.Request.Path} responded with {statusCode} in {responseTime} ms");
            }
        }
    }
}
