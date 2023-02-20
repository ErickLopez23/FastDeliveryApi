using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using FastDeliveryApi.Data;
using FastDeliveryApi.Repositories.Interfaces;

namespace FastDeliveryApi.Repositories;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly FastDeliveryDbContext _dbContext;

    public UnitOfWork(FastDeliveryDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntities();
        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditableEntities()
    {
        IEnumerable<EntityEntry<IAuditableEntity>> entries =
            _dbContext
                .ChangeTracker
                .Entries<IAuditableEntity>();
        foreach (EntityEntry<IAuditableEntity> entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(a => a.CreatedOnUtc)
                            .CurrentValue = DateTime.UtcNow;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(a => a.ModifiedOnUtc)
                            .CurrentValue = DateTime.UtcNow;
            }
        }
    }
}