using DevFlow.Common.Domain;
using DevFlow.Modules.Workspaces.Domain.Workspaces.Entities;

namespace DevFlow.Modules.Workspaces.Domain.Workspaces.DomainServices;

public interface IWorkspaceUniquenessChecker : IDomainService
{
    Task EnsureNameIsUniqueAsync(string name, CancellationToken cancellationToken = default);
}
