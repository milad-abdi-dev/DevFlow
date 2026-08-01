using DevFlow.Common.Application.Messaging;

namespace DevFlow.Modules.Workspaces.Application.Workspaces.CreateWorkspace;

public sealed record CreateWorkspaceCommand(string Name, string Description, Guid OwnerId) : ICommand;
