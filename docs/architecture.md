# Architecture

This document describes the architecture that exists in the repository today. Keep it short and update it when a boundary or major technical decision changes.

## System overview

DevFlow is a workflow and task-management backend built as a modular monolith. It is one deployable ASP.NET Core application, while business capabilities are separated into modules that can evolve independently.

Current state: the shared building blocks and the first `Workspaces` module are being established. The only HTTP endpoint currently mapped is `GET /health`; workspace commands are not exposed through an API yet.

## Repository layout

```text
src/
  API/                         # Composition root and host
  Common/                      # Shared building blocks, not business features
    DevFlow.Common.Domain
    DevFlow.Common.Application
    DevFlow.Common.Infrastructure
    DevFlow.Common.Presentation
  Modules/
    Workspace/                 # Workspaces business capability
      ...Domain
      ...Application
      ...Infrastructure
      ...Presentation
tests/
  UnitTests/
  IntegrationTests/
docs/
```

Each module follows these dependency rules:

```text
Presentation -> Application -> Domain
Infrastructure -> Application + Domain
API -> module and Common projects (composition only)
```

- **Domain** owns entities, value objects, business rules, and domain events. It must not depend on persistence or ASP.NET Core.
- **Application** owns use cases, commands, queries, handlers, validation, and abstractions needed by a use case.
- **Infrastructure** implements persistence and external-service abstractions and registers the module with dependency injection.
- **Presentation** owns minimal API endpoints and maps application results to HTTP responses.
- **API** loads configuration, registers modules and cross-cutting services, and builds the HTTP pipeline. Business logic does not belong here.

Do not reference one module's internals from another module. Cross-module communication should eventually use contracts/integration events; shared technical primitives belong in `Common`.

## Main patterns

- **Vertical slices:** organize application and presentation code by use case, for example `Workspaces/CreateWorkspace/`, instead of by technical type.
- **Tests with each slice:** every new business rule or use case includes behavior-focused unit tests; add integration tests when the slice crosses an external boundary.
- **CQRS with MediatR contracts:** commands change state; queries return data. Both return `Result`/`Result<T>` rather than throwing for expected failures.
- **Validation:** command validators use FluentValidation. The shared validation pipeline converts failures to `ValidationError`.
- **Domain events:** entities collect events; `PublishDomainEventsInterceptor` publishes them through MediatR after EF Core saves successfully.
- **Integration events:** `IEventBus` is backed by MassTransit. It currently uses the in-memory transport, so messages are process-local and not durable.
- **Persistence:** each module owns an EF Core `DbContext` and PostgreSQL schema. Workspaces uses the `workspaces` schema. `IDbConnectionFactory`/Dapper are available for read-side queries.
- **HTTP errors:** `ApiResults.Problem` maps domain error types to RFC-style problem responses (`400`, `404`, `409`, or `500`).

## Runtime infrastructure

| Component | Current role |
| --- | --- |
| PostgreSQL | Transactional module data |
| Redis | Distributed cache; shared infrastructure falls back to memory if Redis cannot connect during registration |
| MassTransit | In-process integration-event transport |
| Serilog + Seq | Structured request/application logs |
| Swagger | API discovery in Development |
| Health checks | PostgreSQL and Redis readiness at `GET /health` |

Package versions are centrally managed in `Directory.Packages.props`. Repository-wide compiler and analyzer settings are in `Directory.Build.props` and `.editorconfig`.

## Current gaps

These pieces exist only partially and should not be treated as finished architecture:

- `CreateWorkspaceCommand` and its validator exist, but the handler is empty.
- No workspace endpoint, entity mapping, repository, or database migration exists yet.
- Common MediatR, validation, endpoint, and infrastructure registration is not wired into `Program.cs` yet.
- Module-specific JSON files exist, but `AddModuleConfiguration` is not called yet.
- Authentication/authorization packages are referenced, but authentication is not configured.
- There are no architecture tests or CI pipeline yet.

Track sequencing and completion in [roadmap.md](roadmap.md), and follow [testing.md](testing.md) for the test strategy; do not turn this document into a task list.
