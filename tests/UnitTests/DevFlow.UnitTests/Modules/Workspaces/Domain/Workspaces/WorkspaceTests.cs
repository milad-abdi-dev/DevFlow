using DevFlow.Modules.Workspaces.Domain.Workspaces;
using FluentAssertions;

namespace DevFlow.UnitTests.Modules.Workspaces.Domain.Workspaces;

public sealed class WorkspaceTests
{
    [Fact]
    public void Create_workspaces()
    {
        string name = "DevFlow";
        string description = "DevFlow is a workflow management project.";
        var ownerId = Guid.NewGuid();

        var workspace = Workspace.Create(name,  description, ownerId);
        
        workspace.Name.Should().Be(name);
        workspace.Description.Should().Be(description);
        workspace.OwnerId.Should().Be(ownerId);
        workspace.State.Should().Be(WorkspaceState.Active);
        workspace.Id.Should().NotBeEmpty();
        workspace.IsDeleted.Should().BeFalse();
    }
    
    [Fact]
    public void Archive_workspaces()
    {
        string name = "DevFlow";
        string description = "DevFlow is a workflow management project.";
        var  ownerId = Guid.NewGuid();
        var workspace = Workspace.Create(name,  description, ownerId);

        workspace.Archive();
        
        workspace.State.Should().Be(WorkspaceState.Archived);
    }
    
    [Fact]
    public void Activate_workspaces()
    {
        string name = "DevFlow";
        string description = "DevFlow is a workflow management project.";
        var ownerId = Guid.NewGuid();
        var workspace = Workspace.Create(name,  description, ownerId);

        workspace.Activate();
        
        workspace.State.Should().Be(WorkspaceState.Active);
    }
    
    
}
