# Testing

Use this guide when adding or changing tests. Tests should describe business behavior and remain useful if implementation details are refactored.

## Required coverage

- Every new business rule or use case requires unit tests in the same change.
- Every bug fix requires a regression test that fails without the fix.
- Cover the successful path, each meaningful failure, and important boundary values.
- Add integration tests when behavior crosses the HTTP, database, cache, messaging, or dependency-injection boundary.

## Unit-test style

- Use xUnit and FluentAssertions.
- Mirror the production namespace and folder below `tests/UnitTests`.
- Name tests as business statements using the existing snake-case style: `<Action>_should_<outcome>_when_<condition>`.
- Structure each test as Arrange, Act, Assert, separated by blank lines; do not add comments for those sections.
- Test one business behavior per test. Use a valid baseline and change only the value relevant to that behavior.
- Assert observable results, state, or errors. Do not test private methods, internal call order, or framework implementation details.
- Prefer real domain objects. Mock only external boundaries or expensive collaborators, not the business logic under test.
- Keep tests deterministic and independent; avoid real time, random behavior, shared mutable state, and execution-order dependencies.

Example:

```csharp
[Fact]
public void Create_workspace_should_have_error_when_name_is_too_short()
{
    var command = new CreateWorkspaceCommand("AB", "Description", Guid.NewGuid());
    var validator = new CreateWorkspaceCommandValidator();

    ValidationResult result = validator.Validate(command);

    result.Errors.Should().Contain(
        error => error.PropertyName == nameof(CreateWorkspaceCommand.Name));
}
```

## Choosing the test level

| Behavior | Test level |
| --- | --- |
| Entity invariants and state transitions | Domain unit test |
| Command/query validation and handler decisions | Application unit test |
| Endpoint contracts, persistence, DI, or infrastructure integration | Integration test |
| Module dependency rules | Architecture test |

## Before finishing

Run the narrowest relevant test project first, then the full suite when practical. Commands and prerequisites are in [development.md](development.md). Report any check that could not be run.
