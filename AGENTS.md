# DevFlow Agent Instructions

## Before changing code

1. Read `docs/architecture.md` and `docs/conventions.md`.
2. Read `docs/roadmap.md` before starting a feature or changing project scope.
3. Read `docs/development.md` before running, testing, configuring, or debugging the application.
4. Read `docs/testing.md` before writing or changing tests.
5. Inspect the relevant implementation and neighboring vertical slices. Do not assume planned features are complete.

## While working

- Follow the module boundaries and dependency rules in `docs/architecture.md`.
- Follow the coding, error-handling, and testing rules in `docs/conventions.md`.
- Add behavior-focused unit tests for every new business rule or use case, following `docs/testing.md`.
- Keep changes focused on the requested task and preserve unrelated user changes.
- Treat the current code as the source of truth when it conflicts with documentation. Report the conflict and update the affected documentation when appropriate.
- Do not add an abstraction, dependency, or cross-module coupling unless the current use case requires it.

## Before completing a task

- Build the solution and run the relevant tests described in `docs/development.md`.
- Clearly report checks that could not be run and why.
- Update `docs/architecture.md` when boundaries or major technical decisions change.
- Update `docs/development.md` when setup, configuration, or commands change.
- Update `docs/testing.md` when the project's testing strategy or style changes.
- Update `docs/roadmap.md` when implementation status or planned scope changes.
