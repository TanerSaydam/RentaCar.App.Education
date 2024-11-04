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
public sealed record UpdateVehicleCommand(
    Guid Id,
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
    IFormFile? File) : IRequest<Result<string>>;

internal sealed class UpdateVehicleCommandHandlerValidator : AbstractValidator<UpdateVehicleCommand>
{
    public UpdateVehicleCommandHandlerValidator()
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

internal sealed class UpdateVehicleCommandHandler(
    IVehicleRepository vehicleRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper
    ) : IRequestHandler<UpdateVehicleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
    {
        Vehicle vehicle = await vehicleRepository.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        if (vehicle is null)
        {
            return Result<string>.Failure("Araç bulunamadı");
        }

        if (vehicle.Plate != request.Plate)
        {
            bool isPlateExists = await vehicleRepository.AnyAsync(p => p.Plate == request.Plate, cancellationToken);
            if (isPlateExists)
            {
                return Result<string>.Failure("Bu plaka daha önce kaydedilmiş");
            }
        }

        mapper.Map(request, vehicle);

        vehicle.FuelType = FuelTypeEnum.FromValue(request.FuelTypeValue);
        vehicle.GearType = GearTypeEnum.FromValue(request.GearTypeValue);

        if (request.File is not null)
        {
            FileService.FileDeleteToServer($"wwwroot/images/{vehicle.CoverImageUrl}");

            string fileName = FileService.FileSaveToServer(request.File, "wwwroot/images/");
            vehicle.CoverImageUrl = fileName;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Araç bilgileri başarıyla güncellendi";
    }
}