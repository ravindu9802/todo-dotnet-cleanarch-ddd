using MediatR;
using Microsoft.Extensions.Logging;

namespace Todo.Application.Behaviours;

public class LoggingPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TRequest>
{
    private readonly ILogger<LoggingPipelineBehaviour<TRequest, TResponse>> _logger;

    public LoggingPipelineBehaviour(ILogger<LoggingPipelineBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting Request {RequestName}, {DateTimeUtc}", typeof(TRequest).Name, DateTime.UtcNow);

        var response = next();

        _logger.LogInformation("Completed Request {RequestName}, {DateTimeUtc}", typeof(TRequest).Name,
            DateTime.UtcNow);

        return response;
    }
}