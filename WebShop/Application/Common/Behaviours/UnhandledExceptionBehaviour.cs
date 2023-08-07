using Microsoft.Extensions.Logging;

namespace WebShop.Application.Common.Behaviours;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull {
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger) {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) {
        try {
            return await next();
        }
        catch ( NotFoundException vex ) {
            var requestName = typeof(TRequest).Name;

            _logger.LogWarning(vex, "WebShop Request: Entity not found {Name} {@Request}", requestName, request);

            throw;
        }
        catch ( ValidationException vex ) {
            var requestName = typeof(TRequest).Name;

            _logger.LogWarning(vex, "WebShop Request: Validation fail at {Name} {@Request}", requestName, request);

            throw;
        }
        catch ( Exception ex ) {
            var requestName = typeof(TRequest).Name;

            _logger.LogError(ex, "WebShop Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);

            throw;
        }
    }
}
