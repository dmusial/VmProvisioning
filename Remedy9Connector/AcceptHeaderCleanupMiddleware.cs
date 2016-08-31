using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Remedy9Connector
{
    public class AcceptHeaderCleanupMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public AcceptHeaderCleanupMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<AcceptHeaderCleanupMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation(context.Request.Path);
            if (context.Request.Path.Value.Contains("remoteserver/remoteserverdescriptor"))
            {
                _logger.LogInformation("Removing accept header");
                context.Request.Headers.Remove("Accept");
            }

            await _next(context);
        }
    }

    public static class AcceptHeaderCleanupMiddlewareExtensions  
    {
        public static IApplicationBuilder UseAcceptHeaderCleanupMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AcceptHeaderCleanupMiddleware>();
        }
    }
}