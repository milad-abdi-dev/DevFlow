using DevFlow.Common.Domain;
using DevFlow.Modules.Workspaces.Domain.Workspaces.Entities;

namespace DevFlow.Modules.Workspaces.Domain.Workspaces;

public interface IWorkspaceRepository : IRepository
{
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);

    Task AddAsync(Workspace workspace, CancellationToken cancellationToken = default);
}
