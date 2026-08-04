using DevFlow.Common.Domain;
using DevFlow.Modules.Workspaces.Domain.Workspaces.Enums;

namespace DevFlow.Modules.Workspaces.Domain.Workspaces.Entities;

public sealed class Workspace : AggregateRoot
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Guid OwnerId { get; private set; }
    public WorkspaceStatus Status { get; private set; }
    public bool IsDeleted { get; private set; }

    public static Workspace Create(string name, string description, Guid ownerId)
    {
        return new Workspace
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            OwnerId = ownerId,
            Status = WorkspaceStatus.Active
        };
    }

    public void Archive()
    {
        Status = WorkspaceStatus.Archived;
    }

    public void Restore()
    {
        Status = WorkspaceStatus.Active;
    }
}
