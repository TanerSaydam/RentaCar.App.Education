using AutoMapper;
using RentaCar.Vehicles.Application.Features.Vehicles;
using RentaCar.Vehicles.Domain.Entities;

namespace RentaCar.Vehicles.Application.Mapping;
public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateVehicleCommand, Vehicle>();
        CreateMap<UpdateVehicleCommand, Vehicle>();
    }
}
