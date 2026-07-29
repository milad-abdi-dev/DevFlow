namespace DevFlow.Common.Domain;

public abstract class AuditableEntity : Entity
{
    public DateTimeOffset CreatedOn { get; set; }

    public DateTimeOffset? ModifiedOn { get; set; }
}
