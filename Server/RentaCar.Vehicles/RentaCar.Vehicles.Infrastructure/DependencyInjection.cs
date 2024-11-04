using GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RentaCar.Vehicles.Domain.Repositories;
using RentaCar.Vehicles.Infrastructure.Context;
using RentaCar.Vehicles.Infrastructure.Repositories;

namespace RentaCar.Vehicles.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
        });

        services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IVehicleRepository, VehicleRepository>();

        //services.Scan(action =>
        //{
        //    action
        //    .FromAssemblies(Assembly.GetExecutingAssembly())
        //    .AddClasses(publicOnly: false)
        //    .AsImplementedInterfaces()
        //    .WithScopedLifetime();
        //});


        services.AddHealthChecks()
        .AddCheck("health-check", () => HealthCheckResult.Healthy())
        .AddDbContextCheck<ApplicationDbContext>()
        ;

        return services;
    }
}
