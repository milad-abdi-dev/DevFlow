using DevFlow.Common.Infrastructure.Interceptors;
using DevFlow.Modules.Workspaces.Application.Abstractions;
using DevFlow.Modules.Workspaces.Domain.Workspaces.DomainServices;
using DevFlow.Modules.Workspaces.Domain.Workspaces.DomainServices.Implementations;
using DevFlow.Modules.Workspaces.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevFlow.Modules.Workspaces.Infrastructure;

public static class WorkspacesModule
{
    public static IServiceCollection AddWorkspacesModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        
        return services;
    }
    
    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WorkspacesDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    configuration.GetConnectionString("Database"),
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Workspaces))
                .AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptor>())
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<WorkspacesDbContext>());
        
        services.AddScoped<IWorkspaceUniquenessChecker, WorkspaceUniquenessChecker>();
    }
}
