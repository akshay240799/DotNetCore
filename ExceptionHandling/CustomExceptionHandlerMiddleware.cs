using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ExceptionHandling
{
    public class CustomExceptionHandlerMiddleware : IMiddleware
    {
		private readonly ILogger _logger;
		public CustomExceptionHandlerMiddleware(ILogger<CustomExceptionHandlerMiddleware> logger) 
		{
			_logger = logger;
		}
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // i.e. 500
				ProblemDetails details = new()
				{
					Status = 500,
					Detail = ex.Message,
					Title = "Error"
				};
				string json = JsonSerializer.Serialize(details);

				_logger.LogError(json);

				context.Response.ContentType = "application/json";
				await context.Response.WriteAsync(json);
			}
        }
    }
}
