using DevFlow.Common.Application.Messaging;
using DevFlow.Common.Domain;
using DevFlow.Modules.Workspaces.Domain.Workspaces;
using DevFlow.Modules.Workspaces.Domain.Workspaces.DomainServices;
using DevFlow.Modules.Workspaces.Domain.Workspaces.Entities;

namespace DevFlow.Modules.Workspaces.Application.Workspaces.CreateWorkspace;

public class CreateWorkspaceCommandHandler : ICommandHandler<CreateWorkspaceCommand>
{
    private readonly IWorkspaceRepository _workspaceRepository;
    private readonly IWorkspaceUniquenessChecker _workspaceUniquenessChecker;

    public CreateWorkspaceCommandHandler(
        IWorkspaceRepository workspaceRepository,
        IWorkspaceUniquenessChecker workspaceUniquenessChecker
        )
    {
        _workspaceRepository = workspaceRepository;
        _workspaceUniquenessChecker = workspaceUniquenessChecker;
    }
    
    public async Task<Result> Handle(CreateWorkspaceCommand request, CancellationToken cancellationToken)
    {
        await _workspaceUniquenessChecker.EnsureNameIsUniqueAsync(request.Name, cancellationToken);
        
        var workspace = Workspace.Create(request.Name, request.Description, request.OwnerId);
        
        await _workspaceRepository.AddAsync(workspace, cancellationToken);
        
        return Result.Success();
    }
}
