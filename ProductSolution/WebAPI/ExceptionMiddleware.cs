using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Net;

namespace WebAPI
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occurred: {ex}");

                var Response = httpContext.Response;
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await Response.WriteAsync("Error Occurred. Please refer the logs.");
            }
        }
    }
}
