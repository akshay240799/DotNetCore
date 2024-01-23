using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Middlewares
{
    // Conventional Middlewares act as singleton 
    public class ConventionalMiddleware
    {
        private readonly RequestDelegate _next;

        // can add only singleton dependencies in contructor
        public ConventionalMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        // can add scoped dependencies in Invoke method to inject into the middleware
        // framework will automatically inject dependecies as per registered service
        public Task Invoke(HttpContext httpContext)
        {
            // add logic here to handle request or response
            httpContext.Response.WriteAsync("In Conventional Middleware.\n");
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ConventionalMiddlewareExtensions
    {
        public static IApplicationBuilder UseConventionalMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ConventionalMiddleware>();
        }
    }
}
