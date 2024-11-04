using AutoMapper;
using FluentValidation;
using GenericFileService.Files;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Http;
using RentaCar.Vehicles.Domain.Entities;
using RentaCar.Vehicles.Domain.Repositories;
using TS.Result;

namespace RentaCar.Vehicles.Application.Features.Vehicles;
public sealed record CreateVehicleCommand(
    string Brand,
    string Model,
    int Year,
    string Plate,
    decimal DailyPrice,
    bool IsActive,
    int FuelTypeValue,
    int GearTypeValue,
    int DoorCount,
    int PassengerCapacity,
    IFormFile File) : IRequest<Result<string>>;

internal sealed class CreateVehicleCommandHandlerValidator : AbstractValidator<CreateVehicleCommand>
{
    public CreateVehicleCommandHandlerValidator()
    {
        RuleFor(p => p.Brand).MinimumLength(3).WithMessage("Marka en az 3 karakter olmalıdır");
        RuleFor(p => p.Model).MinimumLength(3).WithMessage("Model en az 3 karakter olmalıdır");
        RuleFor(p => p.Year).GreaterThanOrEqualTo(2000).WithMessage("Araç 2000 model ve üstü olmalıdır");
        RuleFor(p => p.Plate).MinimumLength(3).WithMessage("Araç plakası en az 3 karakter olmalıdır");
        RuleFor(p => p.DailyPrice).GreaterThanOrEqualTo(500).WithMessage("Araç günlük ücreti en az 500₺ olabilir");
        RuleFor(p => p.DoorCount).GreaterThan(0).WithMessage("Kapı sayısı 0 olamaz");
        RuleFor(p => p.PassengerCapacity).GreaterThan(0).WithMessage("Yolcu kapasitesi 0 olamaz");
    }
}

internal sealed class CreateVehicleCommandHandler(
    IVehicleRepository vehicleRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper
    ) : IRequestHandler<CreateVehicleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
    {
        bool isPlateExists = await vehicleRepository.AnyAsync(p => p.Plate == request.Plate, cancellationToken);
        if (isPlateExists)
        {
            return Result<string>.Failure("Bu plaka daha önce kaydedilmiş");
        }

        Vehicle vehicle = mapper.Map<Vehicle>(request);

        vehicle.FuelType = FuelTypeEnum.FromValue(request.FuelTypeValue);
        vehicle.GearType = GearTypeEnum.FromValue(request.GearTypeValue);

        string fileName = FileService.FileSaveToServer(request.File, "wwwroot/images/");
        vehicle.CoverImageUrl = fileName;

        vehicleRepository.Add(vehicle);
        var result = await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Araç başarıyla kaydedili";
    }
}
