using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace SGSPCSI.API.Middleware
{
    public class ApiExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionHandlingMiddleware> _logger;

        public ApiExceptionHandlingMiddleware(RequestDelegate next, ILogger<ApiExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception while processing {Method} {Path}", context.Request.Method, context.Request.Path);
                await WriteProblemDetailsAsync(context, ex);
            }
        }

        private static Task WriteProblemDetailsAsync(HttpContext context, Exception exception)
        {
            var statusCode = exception switch
            {
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                InvalidOperationException => (int)HttpStatusCode.Conflict,
                _ => (int)HttpStatusCode.InternalServerError
            };

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;

            var problem = new ProblemDetails
            {
                Status = statusCode,
                Title = statusCode switch
                {
                    400 => "Bad Request",
                    404 => "Not Found",
                    409 => "Conflict",
                    _ => "An unexpected error occurred"
                },
                Detail = exception.Message,
                Instance = context.Request.Path
            };

            return context.Response.WriteAsJsonAsync(problem);
        }
    }
}