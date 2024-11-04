using GenericRepository;
using MediatR;
using RentaCar.Vehicles.Domain.Entities;
using RentaCar.Vehicles.Domain.Repositories;
using TS.Result;

namespace RentaCar.Vehicles.Application.Features.Vehicles;
public sealed record DeleteVehicleByIdCommand(
    Guid Id) : IRequest<Result<string>>;

internal sealed class DeleteVehicleByIdCommandHandler(
    IVehicleRepository vehicleRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteVehicleByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteVehicleByIdCommand request, CancellationToken cancellationToken)
    {
        Vehicle vehicle = await vehicleRepository.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        if (vehicle is null)
        {
            return Result<string>.Failure("Araç bulunamadı");
        }

        vehicle.IsDeleted = true;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Araç kaydı başarıyla silindi";
    }
}
