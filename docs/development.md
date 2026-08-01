# Development

## Prerequisites

- .NET 10 SDK
- Docker Desktop or Docker Engine with Compose
- An IDE with C# support (Visual Studio, Rider, or VS Code)

Integration tests use Testcontainers, so Docker must be running even when the application itself is launched with `dotnet run`.

## Get started

From the repository root:

```bash
dotnet restore DevFlow.slnx
dotnet build DevFlow.slnx
dotnet test DevFlow.slnx
```

Warnings fail the build by design.

### Run everything in containers

The intended full-stack command is:

```bash
docker compose up --build
```

Current limitation: the API Dockerfile paths do not match the repository-root build context in `docker-compose.yml`, so the API image may not build until those paths are corrected. The dependency-only and host-run workflow below remains useful in the meantime.

Default local services:

| Service | Address |
| --- | --- |
| API | `http://localhost:5000` |
| Health | `http://localhost:5000/health` |
| Swagger | `http://localhost:5000/swagger` |
| PostgreSQL | `localhost:54320` |
| Redis | `localhost:6379` |
| Seq UI | `http://localhost:8081` |

The checked-in PostgreSQL, Redis, and Seq credentials are development-only defaults. Do not reuse them in a deployed environment.

Stop containers with `docker compose down`. PostgreSQL data persists in `.containers/db`; `docker compose down` does not remove it.

### Run the API from the host

Start its dependencies:

```bash
docker compose up -d devflow.database devflow.redis devflow.seq
```

The Development settings use Docker service names, so override the two connection strings when the API runs on the host:

```bash
ConnectionStrings__Database='Host=localhost;Port=54320;Database=devflow;Username=postgres;Password=postgres;Include Error Detail=true' \
ConnectionStrings__Cache='localhost:6379' \
dotnet run --project src/API/DevFlow.Api/DevFlow.Api.csproj
```

The launch profile listens on `http://localhost:5281` (and `https://localhost:7086` for the HTTPS profile). Swagger is available only in the Development environment.

## Testing

Every new feature, business rule, and bug fix must include tests. Follow the project style and test-level guidance in [testing.md](testing.md).

```bash
# Everything
dotnet test DevFlow.slnx

# Fast unit tests only
dotnet test tests/UnitTests/DevFlow.UnitTests/DevFlow.UnitTests.csproj

# Integration tests (requires Docker)
dotnet test tests/IntegrationTests/DevFlow.IntegrationTests/DevFlow.IntegrationTests.csproj
```

Integration tests start isolated PostgreSQL and Redis containers automatically. They do not use the Compose containers or checked-in connection strings.

## Configuration

- Global settings: `src/API/DevFlow.Api/appsettings*.json`
- Reserved per-module settings: `src/API/DevFlow.Api/modules.<module>*.json` (the loader exists but is not called yet)
- Local overrides: environment variables or .NET user secrets
- Package versions: `Directory.Packages.props`
- Build/analyzer policy: `Directory.Build.props` and `.editorconfig`

Use double underscores for nested environment keys, for example `ConnectionStrings__Database`. Never place production secrets in tracked JSON files.

## Common troubleshooting

- **`dotnet` not found:** install the .NET 10 SDK and verify with `dotnet --info`.
- **Integration tests cannot start:** verify Docker is running and available to the current shell.
- **Health check is unhealthy:** confirm PostgreSQL and Redis are reachable using the configured connection strings.
- **Host-run API cannot resolve `devflow.database` or `devflow.redis`:** use the localhost overrides shown above.
- **Compose cannot find `DevFlow.Api.csproj`:** align the Dockerfile `COPY` paths with its repository-root Compose context.
- **Build fails on a warning:** follow the diagnostic and `.editorconfig`; warnings are treated as errors.

For boundaries and data flow, read [architecture.md](architecture.md). For coding decisions, read [conventions.md](conventions.md).
