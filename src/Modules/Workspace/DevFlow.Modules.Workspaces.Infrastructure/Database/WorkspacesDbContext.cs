using System.Data.Common;
using DevFlow.Modules.Workspaces.Application.Abstractions;
using DevFlow.Modules.Workspaces.Domain.Workspaces.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DevFlow.Modules.Workspaces.Infrastructure.Database;

public class WorkspacesDbContext(DbContextOptions<WorkspacesDbContext> options) 
    : DbContext(options), IUnitOfWork
{
    internal DbSet<Workspace> Workspaces { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Workspaces);

        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
    
    public async Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (Database.CurrentTransaction is not null)
        {
            await Database.CurrentTransaction.DisposeAsync();
        }

        return (await Database.BeginTransactionAsync(cancellationToken)).GetDbTransaction();
    }
}
