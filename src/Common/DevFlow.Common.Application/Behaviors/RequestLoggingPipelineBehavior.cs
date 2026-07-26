using DevFlow.Common.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace DevFlow.Common.Application.Behaviors;

internal sealed partial class RequestLoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger
    )
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string moduleName = GetModuleName(typeof(TRequest).FullName!);
        string requestName = typeof(TRequest).Name;

        using (LogContext.PushProperty("Module", moduleName))
        {
            LogProcessingRequest(logger, requestName);

            TResponse result = await next(cancellationToken);

            if (result.IsSuccess)
            {
                LogCompletedRequest(logger, requestName);
            }
            else
            {
                using (LogContext.PushProperty("Error", result.Error, true))
                {
                    LogCompletedRequestWithError(logger, requestName);
                }
            }

            return result;
        }
    }

    private static string GetModuleName(string requestName) => requestName.Split('.')[2];
    [LoggerMessage(LogLevel.Information, "Processing request {RequestName}")]
    static partial void LogProcessingRequest(ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger, string RequestName);

    [LoggerMessage(LogLevel.Information, "Completed request {RequestName}")]
    static partial void LogCompletedRequest(ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger, string RequestName);

    [LoggerMessage(LogLevel.Error, "Completed request {RequestName} with error")]
    static partial void LogCompletedRequestWithError(ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger, string RequestName);
}
