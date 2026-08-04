using DevFlow.Api.Extensions;
using DevFlow.Common.Application;
using DevFlow.Common.Domain;
using DevFlow.Common.Infrastructure;
using DevFlow.Modules.Workspaces.Infrastructure;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(static type =>
        type.FullName?.Replace('+', '.') ?? type.Name);
});

builder.Services.AddApplication([
    DevFlow.Modules.Workspaces.Application.AssemblyReference.Assembly
]);

string databaseConnectionString = builder.Configuration.GetConnectionString("Database")!;
string redisConnectionString = builder.Configuration.GetConnectionString("Cache")!;

builder.Services.AddInfrastructure(
    [],
    databaseConnectionString,
    redisConnectionString);

builder.Configuration.AddModuleConfiguration(["workspaces"]);

builder.Services.AddHealthChecks()
    .AddNpgSql(databaseConnectionString)
    .AddRedis(redisConnectionString);

builder.Services.AddWorkspacesModule(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseSerilogRequestLogging();

await app.RunAsync();

#pragma warning disable CA1515 // WebApplicationFactory requires a publicly accessible entry point.
public partial class Program;
#pragma warning restore CA1515
