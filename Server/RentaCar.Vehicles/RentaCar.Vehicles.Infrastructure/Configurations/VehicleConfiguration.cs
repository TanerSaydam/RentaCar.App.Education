using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentaCar.Vehicles.Domain.Entities;

namespace RentaCar.Vehicles.Infrastructure.Configurations;
internal sealed class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.Property(p => p.DailyPrice).HasColumnType("money");
        builder.Property(p => p.GearType).HasConversion(gearType => gearType.Value, value => GearTypeEnum.FromValue(value));
        builder.Property(p => p.FuelType).HasConversion(fuelType => fuelType.Value, value => FuelTypeEnum.FromValue(value));

        builder.HasIndex(x => x.Plate).IsUnique();

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
