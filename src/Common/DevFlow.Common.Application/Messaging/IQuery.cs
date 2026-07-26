using DevFlow.Common.Domain;
using MediatR;

namespace DevFlow.Common.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
