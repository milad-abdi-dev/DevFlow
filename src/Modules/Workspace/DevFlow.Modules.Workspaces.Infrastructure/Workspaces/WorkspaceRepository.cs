using DevFlow.Modules.Workspaces.Domain.Workspaces;
using DevFlow.Modules.Workspaces.Domain.Workspaces.Entities;
using DevFlow.Modules.Workspaces.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace DevFlow.Modules.Workspaces.Infrastructure.Workspaces;

public class WorkspaceRepository(WorkspacesDbContext context) : IWorkspaceRepository
{
    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await context.Workspaces.AnyAsync(x => x.Name == name, cancellationToken);
    }

    public async Task AddAsync(Workspace workspace, CancellationToken cancellationToken = default)
    {
        await context.Workspaces.AddAsync(workspace, cancellationToken);
    }
}
