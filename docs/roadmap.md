# Roadmap

This is a lightweight sequence, not a commitment to dates. Keep only the next few concrete milestones detailed; split work into issues when implementation starts.

Legend: `[x]` implemented, `[~]` partial/in progress, `[ ]` planned.

## Foundation

- [x] Create modular-monolith solution structure.
- [x] Add shared domain primitives (`Entity`, `AggregateRoot`, domain events, result/error types).
- [x] Add command/query contracts, validation and logging pipeline behaviors.
- [x] Add shared PostgreSQL, Redis, MassTransit, Serilog, and endpoint building blocks.
- [x] Add PostgreSQL and Redis health checks.
- [x] Add unit and Testcontainers-based integration test projects.
- [~] Wire all shared infrastructure, MediatR behaviors, validators, and endpoint discovery into the API host.
- [ ] Align the API Dockerfile paths with the Compose build context.
- [ ] Add automated module-boundary/architecture tests.
- [ ] Add CI for restore, build, test, and formatting/analyzer checks.

## Workspaces — current milestone

- [x] Model workspace creation and active/archived state transitions.
- [x] Define `CreateWorkspaceCommand` and input validation.
- [ ] Add EF Core workspace mapping and initial migration.
- [ ] Implement persistence abstraction and `CreateWorkspaceCommandHandler`.
- [ ] Expose the create-workspace endpoint and consistent error responses.
- [ ] Add application and API integration tests for workspace creation.
- [ ] Add get, update, archive/activate, and delete workspace use cases.
- [ ] Define membership, roles, and workspace authorization rules.

## Next capabilities

Build these incrementally after the Workspaces vertical slice works end to end:

1. Projects within a workspace.
2. Configurable workflows and statuses.
3. Tasks/issues and state transitions.
4. Assignment and workspace/project membership.
5. Comments and activity history.
6. Notifications.

## Later platform work

- Authentication and authorization.
- Durable cross-module messaging with an outbox/inbox strategy.
- Observability beyond local Seq logging.
- Production deployment configuration and secret management.
- Performance, resilience, and security hardening driven by measured needs.

## Scope rule

Complete one thin vertical slice—including domain behavior, behavior-focused unit tests, persistence, endpoint, and required integration tests—before expanding a capability. Add abstractions only when a current use case needs them.
