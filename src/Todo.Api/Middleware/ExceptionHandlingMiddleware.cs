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
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception occured: {Message}", exception.Message);

            var exceptionDetails = GetExceptionDetails(exception);

            ProblemDetails details = new()
            {
                Title = exceptionDetails.Title,
                Type = exceptionDetails.Type,
                Status = exceptionDetails.Status,
                Detail = exceptionDetails.Detail,
            };

            if (exceptionDetails.Errors is not null)
                details.Extensions.Add("errors", exceptionDetails.Errors);

            context.Response.StatusCode = exceptionDetails.Status;
            await context.Response.WriteAsJsonAsync(details);
        }
    }

    private static ExceptionDetails GetExceptionDetails(Exception exception)
    {
        return exception switch
        {
            _ => new ExceptionDetails(
                Status: StatusCodes.Status500InternalServerError,
                Type: "InternalServerError",
                Title: "Internal server error",
                Detail: exception.Message,
                Errors: null)
        };
    }

    internal record ExceptionDetails(
        int Status,
        string Type,
        string Title,
        string Detail,
        IEnumerable<object>? Errors);
}
