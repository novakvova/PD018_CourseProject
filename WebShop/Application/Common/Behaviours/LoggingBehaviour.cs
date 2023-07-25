﻿using WebShop.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace WebShop.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull {
    private readonly ILogger _logger;

    public LoggingBehaviour(ILogger<TRequest> logger) {
        _logger = logger;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken) {
        var requestName = typeof(TRequest).Name;
        string? userName = string.Empty;

        _logger.LogInformation("WebShop Request: {Name} {@UserName} {@Request}",
            requestName, userName, request);
    }
}
