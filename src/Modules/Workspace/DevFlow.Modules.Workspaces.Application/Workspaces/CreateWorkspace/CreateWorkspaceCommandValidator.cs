using FluentValidation;

namespace DevFlow.Modules.Workspaces.Application.Workspaces.CreateWorkspace;

public class CreateWorkspaceCommandValidator : AbstractValidator<CreateWorkspaceCommand>
{
    public CreateWorkspaceCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Workspace name is required.")
            .MaximumLength(100)
            .WithMessage("Workspace name must not exceed 100 characters.")
            .MinimumLength(3)
            .WithMessage("Workspace name must contain at least 3 characters.")
            .Matches(@"^[a-zA-Z0-9\s]+$")
            .WithMessage("Workspace name can only contain letters, numbers, and spaces.");

        RuleFor(c => c.Description)
            .NotEmpty()
            .WithMessage("Workspace description is required.")
            .MaximumLength(500)
            .WithMessage("Workspace description must not exceed 500 characters.")
            .MinimumLength(3)
            .WithMessage("Workspace description must contain at least 3 characters.")
            .Matches(@"^[a-zA-Z0-9\s.,!?'-]+$")
            .WithMessage("Workspace description can only contain letters, numbers, spaces, and basic punctuation.");

        RuleFor(c => c.OwnerId)
            .NotEmpty()
            .WithMessage("Workspace owner ID is required.");
    }
}
