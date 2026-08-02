namespace DevFlow.Common.Domain;

public class DomainException(Error nameNotUnique) : Exception
{
    public Error Error { get; } = nameNotUnique;
}
