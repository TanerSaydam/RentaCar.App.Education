using GenericRepository;
using RentaCar.Vehicles.Domain.Entities;
using RentaCar.Vehicles.Domain.Repositories;
using RentaCar.Vehicles.Infrastructure.Context;

namespace RentaCar.Vehicles.Infrastructure.Repositories;
internal sealed class VehicleRepository : Repository<Vehicle, ApplicationDbContext>, IVehicleRepository
{
    public VehicleRepository(ApplicationDbContext context) : base(context)
    {
    }
}
