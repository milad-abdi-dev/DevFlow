using DevFlow.Modules.Workspaces.Domain.Workspaces.Entities;

namespace DevFlow.Modules.Workspaces.Domain.Workspaces.DomainServices;

public interface IWorkspaceUniquenessChecker
{
    Task EnsureNameIsUniqueAsync(string name, CancellationToken cancellationToken = default);
}
