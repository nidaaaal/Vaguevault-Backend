using Azure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace VagueVault.Backend.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleware(RequestDelegate next,IHostEnvironment hostEnvironment,ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;   
            _environment = hostEnvironment;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
               await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var response = new {
                    StatusCodes = httpContext.Response.StatusCode,
                    Message = _environment.IsDevelopment() ? ex.Message : "Internal server error",
                    Details = _environment.IsDevelopment() ? ex.StackTrace : null
                };
                await httpContext.Response.WriteAsJsonAsync(response);
            }
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
