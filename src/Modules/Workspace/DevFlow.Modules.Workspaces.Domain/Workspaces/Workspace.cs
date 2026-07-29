using DevFlow.Common.Domain;

namespace DevFlow.Modules.Workspaces.Domain.Workspaces;

public sealed class Workspace : AggregateRoot
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Guid  OwnerId { get; private set; }
    public WorkspaceState State { get; private set; }
    public bool IsDeleted { get; private set; }

    public static Workspace Create(string name, string description, Guid ownerId)
    {
        return new Workspace
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            OwnerId = ownerId,
            State = WorkspaceState.Active
        };
    }

    public void Archive()
    {
        State = WorkspaceState.Archived;
    }

    public void Activate()
    {
        State = WorkspaceState.Active;
    }
}

public enum WorkspaceState
{
    Active,
    Archived,
    Deleted
}
