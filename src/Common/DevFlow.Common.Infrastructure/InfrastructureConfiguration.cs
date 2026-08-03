using System.Reflection;
using DevFlow.Common.Application.Caching;
using DevFlow.Common.Application.Clock;
using DevFlow.Common.Application.Data;
using DevFlow.Common.Application.EventBus;
using DevFlow.Common.Domain;
using DevFlow.Common.Infrastructure.Caching;
using DevFlow.Common.Infrastructure.Clock;
using DevFlow.Common.Infrastructure.Data;
using DevFlow.Common.Infrastructure.Interceptors;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using StackExchange.Redis;

namespace DevFlow.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        Action<IRegistrationConfigurator>[] moduleConfigureConsumers,
        string databaseConnectionString,
        string redisConnectionString,
        Assembly[] moduleAssemblies)
    {
        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);

        services.TryAddScoped<IDbConnectionFactory, DbConnectionFactory>();

        services.TryAddSingleton<PublishDomainEventsInterceptor>();

        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();

        try
        {
            IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
            services.TryAddSingleton(connectionMultiplexer);

            services.AddStackExchangeRedisCache(options =>
                options.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer));
        }
        catch
        {
            services.AddDistributedMemoryCache();
        }

        services.TryAddSingleton<ICacheService, CacheService>();

        services.TryAddSingleton<IEventBus, EventBus.EventBus>();

        services.AddMassTransit(configure =>
        {
            foreach (Action<IRegistrationConfigurator> configureConsumer in moduleConfigureConsumers)
            {
                configureConsumer(configure);
            }

            configure.SetKebabCaseEndpointNameFormatter();

            configure.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });

        services.Scan(scan =>
            scan.FromAssemblies(moduleAssemblies)
                .AddClasses(classes => classes.AssignableTo<IRepository>())
                .As<IRepository>()
                .WithScopedLifetime());
        
        services.Scan(scan =>
            scan.FromAssemblies(moduleAssemblies)
                .AddClasses(classes => classes.AssignableTo<IDomainService>())
                .As<IDomainService>()
                .WithScopedLifetime());

        return services;
    }
}
