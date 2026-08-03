using DevFlow.Common.Application.Messaging;
using DevFlow.Common.Domain;
using DevFlow.Modules.Workspaces.Domain.Workspaces;
using DevFlow.Modules.Workspaces.Domain.Workspaces.DomainServices;
using DevFlow.Modules.Workspaces.Domain.Workspaces.Entities;

namespace DevFlow.Modules.Workspaces.Application.Workspaces.CreateWorkspace;

public class CreateWorkspaceCommandHandler(
    IWorkspaceRepository workspaceRepository,
    IWorkspaceUniquenessChecker workspaceUniquenessChecker)
    : ICommandHandler<CreateWorkspaceCommand>
{
    public async Task<Result> Handle(CreateWorkspaceCommand request, CancellationToken cancellationToken)
    {
        await workspaceUniquenessChecker.EnsureNameIsUniqueAsync(request.Name, cancellationToken);
        
        var workspace = Workspace.Create(request.Name, request.Description, request.OwnerId);
        
        await workspaceRepository.AddAsync(workspace, cancellationToken);
        
        return Result.Success();
    }
}
