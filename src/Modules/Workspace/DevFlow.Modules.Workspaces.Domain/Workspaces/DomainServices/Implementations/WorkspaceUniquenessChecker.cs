using DevFlow.Common.Domain;

namespace DevFlow.Modules.Workspaces.Domain.Workspaces.DomainServices.Implementations;

public class WorkspaceUniquenessChecker(IWorkspaceRepository workspaceRepository) : IWorkspaceUniquenessChecker
{
    public async Task EnsureNameIsUniqueAsync(string name, CancellationToken cancellationToken = default)
    {
        if (await workspaceRepository.ExistsByNameAsync(name, cancellationToken))
        {
            throw new DomainException(WorkspaceErrors.NameNotUnique);
        }
    }
}
