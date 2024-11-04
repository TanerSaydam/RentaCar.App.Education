using GenericRepository;
using Microsoft.EntityFrameworkCore;
using RentaCar.Vehicles.Domain.Abstractions;
using RentaCar.Vehicles.Domain.Entities;

namespace RentaCar.Vehicles.Infrastructure.Context;
internal sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<Entity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(p => p.CreatedDate)
                    .CurrentValue = DateTime.Now;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property(p => p.UpdatedDate)
                    .CurrentValue = DateTime.Now;
            }
        }


        return base.SaveChangesAsync(cancellationToken);
    }
}
