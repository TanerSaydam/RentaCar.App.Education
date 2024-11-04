using MediatR;
using RentaCar.Vehicles.Domain.Entities;
using RentaCar.Vehicles.Domain.Repositories;

namespace RentaCar.Vehicles.Application.Features.Vehicles;
public sealed record GetAllVehicleQuery() : IRequest<IQueryable<Vehicle>>;

internal sealed class GetAllVehicleQueryHandler(
    IVehicleRepository vehicleRepository) : IRequestHandler<GetAllVehicleQuery, IQueryable<Vehicle>>
{
    public async Task<IQueryable<Vehicle>> Handle(GetAllVehicleQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var vehicles = vehicleRepository.GetAll();

        return vehicles;
    }
}
