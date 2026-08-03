using System.Reflection;

namespace DevFlow.Modules.Workspaces.Infrastructure;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
