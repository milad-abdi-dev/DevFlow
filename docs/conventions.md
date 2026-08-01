# Conventions

Follow the codebase first. `.editorconfig`, `Directory.Build.props`, and existing neighboring slices are the executable source of truth; this file records the decisions that are easy to miss.

## General rules

- Target .NET 10 with nullable reference types and implicit usings enabled.
- Treat compiler, analyzer, and code-style warnings as errors. Fix warnings instead of broadly suppressing them; add a narrow suppression with a reason only when required by a framework.
- Use four spaces, file-scoped namespaces, braces, and explicit access modifiers.
- Use `PascalCase` for types and public members, `camelCase` for parameters and locals, `_camelCase` for private fields, and prefix interfaces with `I`.
- Prefer clear names over comments. Comments should explain a non-obvious reason or constraint, not restate code.
- Keep changes scoped. Do not refactor unrelated code while implementing a feature.

## Modules and use cases

- A business capability belongs under `src/Modules/<Module>/` with Domain, Application, Infrastructure, and Presentation projects.
- Put a use case in one vertical-slice directory, such as `Application/Workspaces/CreateWorkspace/`.
- Name messages and handlers consistently: `<Action><Subject>Command`, `<Action><Subject>Query`, and the corresponding `...Handler` and `...Validator`.
- Commands implement `ICommand` or `ICommand<T>`; queries implement `IQuery<T>`. Handlers use the matching shared handler interface.
- Keep the API project as the composition root. A module exposes registration through one public module extension (for example, `AddWorkspacesModule`).
- Never couple modules through another module's Infrastructure or Domain implementation.

## Domain and errors

- Put business invariants and state transitions on aggregates/entities, not in endpoints or EF configurations.
- Use private setters and named factory/behavior methods when constructing or changing domain state.
- Return `Result`/`Result<T>` for expected business failures. Use `Error.NotFound`, `Conflict`, `Problem`, or `Failure` with stable codes in `<Context>.<Reason>` form.
- Throw exceptions only for unexpected failures or programmer errors.
- Raise domain events for meaningful state changes that require in-process reactions. Use integration events only for communication across module boundaries.
- Use UTC for persisted timestamps and event occurrence times.

## Data and HTTP

- A module owns its tables, EF Core `DbContext`, migrations, and PostgreSQL schema. Other modules must not query those tables directly.
- Use EF Core for writes. Use Dapper through `IDbConnectionFactory` when a query benefits from direct SQL.
- Pass `CancellationToken` through async APIs.
- Implement endpoints with `IEndpoint`; keep endpoints thin: bind input, send a command/query, and map the result.
- Map expected failures through `ApiResults.Problem` so error responses stay consistent.
- Never commit credentials. Use environment variables or user secrets outside the local Docker defaults.

## Tests

- Unit tests mirror the production namespace under `tests/UnitTests` and cover business rules and validation without infrastructure.
- Integration tests live under `tests/IntegrationTests` and exercise the real API with disposable PostgreSQL and Redis Testcontainers.
- Every new feature, business rule, and bug fix requires tests at the lowest useful level in the same change.
- Name tests after observable business behavior, not implementation details.
- Tests must be independent and deterministic; do not depend on execution order or shared mutable state.
- Follow the naming, structure, mocking, and coverage rules in [testing.md](testing.md).

## Documentation

- Update `architecture.md` for structural decisions, `development.md` for setup/commands, `testing.md` for test strategy, and `roadmap.md` when delivery status changes.
- Document only current facts or clearly label future intent. Link to source instead of copying implementation details.
- For a decision with meaningful alternatives or long-term consequences, add an ADR later rather than expanding these quick-start documents.

## Before finishing a change

1. Build the solution.
2. Run the relevant tests, then the full suite when practical.
3. Confirm module dependency rules still hold.
4. Update only the documentation affected by the change.
