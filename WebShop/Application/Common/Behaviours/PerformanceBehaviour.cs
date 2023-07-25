using System.Diagnostics;
using WebShop.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace WebShop.Application.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull {
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;

    public PerformanceBehaviour(
        ILogger<TRequest> logger) {
        _timer = new Stopwatch();

        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if ( elapsedMilliseconds > 500 ) {
            var requestName = typeof(TRequest).Name;
            var userName = string.Empty;

            _logger.LogWarning("WebShop Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserName} {@Request}",
                requestName, elapsedMilliseconds, userName, request);
        }

        return response;
    }
}
