using DevFlow.Common.Domain;

namespace DevFlow.Modules.Workspaces.Domain.Workspaces;

public static class WorkspaceErrors
{
    public static readonly Error NameNotUnique =
        Error.Conflict("Workspace.NameNotUnique", "Workspace name must be unique.");
}
