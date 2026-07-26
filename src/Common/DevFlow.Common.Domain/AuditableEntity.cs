namespace DevFlow.Common.Domain;

public abstract class AuditableEntity : Entity
{
    public DateTime CreatedOnUtc { get; set; }

    public DateTime? ModifiedOnUtc { get; set; }
}
