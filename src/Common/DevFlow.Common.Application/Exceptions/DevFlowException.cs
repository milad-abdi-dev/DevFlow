using DevFlow.Common.Domain;

namespace DevFlow.Common.Application.Exceptions;

public sealed class DevFlowException(string requestName, Error? error = null, Exception? innerException = null)
    : Exception("Application exception", innerException)
{
    public string RequestName { get; } = requestName;

    public Error? Error { get; } = error;
}
