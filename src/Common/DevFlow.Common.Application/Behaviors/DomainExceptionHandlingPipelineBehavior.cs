using DevFlow.Common.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DevFlow.Common.Application.Behaviors;

public class DomainExceptionHandlingPipelineBehavior<TRequest, TResponse>(
    ILogger<DomainExceptionHandlingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, Result>,
        IPipelineBehavior<TRequest, Result<TResponse>>
    where TRequest : class
{
    public async Task<Result> Handle(TRequest request, RequestHandlerDelegate<Result> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next(cancellationToken);
        }
        catch (DomainException domainException)
        {
            logger.LogError(domainException, "Domain exception for {RequestName}", typeof(TRequest).Name);

            return Result.Failure(domainException.Error);
        }
    }

    public async Task<Result<TResponse>> Handle(TRequest request, RequestHandlerDelegate<Result<TResponse>> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next(cancellationToken);
        }
        catch (DomainException domainException)
        {
            logger.LogError(domainException, "Domain exception for {RequestName}", typeof(TRequest).Name);

            return Result.Failure<TResponse>(domainException.Error);
        }
    }
}
