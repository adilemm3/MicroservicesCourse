using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformService.Infrastructure.Tools.Behaviors;
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("----- Handling command or query {CommandOrQueryName} ({@CommandOrQuery})", request.GetType().Name, request);
        var response = await next();
        _logger.LogInformation("----- Command {CommandOrQueryName} handled - response: {@Response}", request.GetType().Name, response);

        return response;
    }
}
