using DevFlow.Common.Application.Messaging;
using DevFlow.Common.Domain;

namespace DevFlow.Modules.Workspaces.Application.Workspaces.CreateWorkspace;

public sealed record CreateWorkspaceCommand(string Name, string Description, Guid OwnerId) : ICommand;
