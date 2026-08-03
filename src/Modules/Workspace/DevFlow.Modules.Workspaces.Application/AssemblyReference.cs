using System.Reflection;

namespace DevFlow.Modules.Workspaces.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
