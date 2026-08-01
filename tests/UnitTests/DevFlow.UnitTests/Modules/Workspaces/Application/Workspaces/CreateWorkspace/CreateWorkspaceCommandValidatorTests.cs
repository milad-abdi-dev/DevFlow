using DevFlow.Modules.Workspaces.Application.Workspaces.CreateWorkspace;
using FluentAssertions;
using FluentValidation.Results;

namespace DevFlow.UnitTests.Modules.Workspaces.Application.Workspaces.CreateWorkspace;

public class CreateWorkspaceCommandValidatorTests
{
    [Fact]
    public void Create_workspace_should_have_error_when_name_is_empty()
    {
        var command = new CreateWorkspaceCommand("", "Description", Guid.NewGuid());
        var validator = new CreateWorkspaceCommandValidator();
        
        ValidationResult? result = validator.Validate(command);
        
        result.Errors.Should().Contain(e => e.PropertyName == nameof(CreateWorkspaceCommand.Name));
    }
    
    [Fact]
    public void Create_workspace_should_have_error_when_name_has_more_than_100_characters()
    {
        var command = new CreateWorkspaceCommand("A".PadRight(101), "Description", Guid.NewGuid());
        var validator = new CreateWorkspaceCommandValidator();
        
        ValidationResult? result = validator.Validate(command);
        
        result.Errors.Should().Contain(e => e.PropertyName == nameof(CreateWorkspaceCommand.Name));
    }

    [Fact]
    public void Create_workspace_should_have_error_when_name_has_less_than_3_characters()
    {
        var command = new CreateWorkspaceCommand("AB", "Description", Guid.NewGuid());
        var validator = new CreateWorkspaceCommandValidator();

        ValidationResult result = validator.Validate(command);

        result.Errors.Should().Contain(e => e.PropertyName == nameof(CreateWorkspaceCommand.Name));
    }

    [Fact]
    public void Create_workspace_should_have_error_when_name_has_unsupported_characters()
    {
        var command = new CreateWorkspaceCommand("DevFlow@", "Description", Guid.NewGuid());
        var validator = new CreateWorkspaceCommandValidator();

        ValidationResult result = validator.Validate(command);

        result.Errors.Should().Contain(e => e.PropertyName == nameof(CreateWorkspaceCommand.Name));
    }

    [Fact]
    public void Create_workspace_should_have_error_when_description_is_empty()
    {
        var command = new CreateWorkspaceCommand("Name", "", Guid.NewGuid());
        var validator = new CreateWorkspaceCommandValidator();
        
        ValidationResult? result = validator.Validate(command);
        
        result.Errors.Should().Contain(e => e.PropertyName == nameof(CreateWorkspaceCommand.Description));
    }
    
    [Fact]
    public void Create_workspace_should_have_error_when_description_has_more_than_500_characters()
    {
        var command = new CreateWorkspaceCommand("Name", "A".PadRight(501), Guid.NewGuid());
        var validator = new CreateWorkspaceCommandValidator();
        
        ValidationResult? result = validator.Validate(command);
        
        result.Errors.Should().Contain(e => e.PropertyName == nameof(CreateWorkspaceCommand.Description));
    }

    [Fact]
    public void Create_workspace_should_have_error_when_description_has_less_than_3_characters()
    {
        var command = new CreateWorkspaceCommand("Name", "AB", Guid.NewGuid());
        var validator = new CreateWorkspaceCommandValidator();

        ValidationResult result = validator.Validate(command);

        result.Errors.Should().Contain(e => e.PropertyName == nameof(CreateWorkspaceCommand.Description));
    }

    [Fact]
    public void Create_workspace_should_have_error_when_description_has_unsupported_characters()
    {
        var command = new CreateWorkspaceCommand("Name", "Description@", Guid.NewGuid());
        var validator = new CreateWorkspaceCommandValidator();

        ValidationResult result = validator.Validate(command);

        result.Errors.Should().Contain(e => e.PropertyName == nameof(CreateWorkspaceCommand.Description));
    }

    [Fact]
    public void Create_workspace_should_have_error_when_owner_id_has_no_value()
    {
        var command = new CreateWorkspaceCommand("Name", "Description", Guid.Empty);
        var validator = new CreateWorkspaceCommandValidator();
        
        ValidationResult? result = validator.Validate(command);
        
        result.Errors.Should().Contain(e => e.PropertyName == nameof(CreateWorkspaceCommand.OwnerId));
    }

    [Fact]
    public void Create_workspace_should_be_valid_when_all_values_follow_business_rules()
    {
        var command = new CreateWorkspaceCommand(
            "DevFlow 2026",
            "Workflow planning, made simple!",
            Guid.NewGuid());
        var validator = new CreateWorkspaceCommandValidator();

        ValidationResult result = validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }
}
