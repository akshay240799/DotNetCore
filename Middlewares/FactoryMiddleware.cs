using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Middlewares
{
    public class FactoryMiddleware : IMiddleware
    {
        // Create a constructor to inject dependencies to this middleware
        //private readonly ILogger _logger;
        //public FactoryMiddleware(ILogger logger)
        //{
        //    _logger = logger;
        //}
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("In Factory Middleware.\n");
            await next(context);
            await context.Response.WriteAsync("In Factory Middleware.\n");
        }
    }
}
