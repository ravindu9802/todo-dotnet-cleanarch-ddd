using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Todo.Api.Middleware;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ProblemDetails details = new()
            {
                Title = "Internal server error",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Status = context.Response.StatusCode,
                Detail = e.Message,
            };

            await context.Response.WriteAsJsonAsync(details);
        }
    }
}
