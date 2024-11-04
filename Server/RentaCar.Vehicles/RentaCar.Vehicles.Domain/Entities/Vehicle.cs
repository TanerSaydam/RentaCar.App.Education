using Ardalis.SmartEnum;
using RentaCar.Vehicles.Domain.Abstractions;

namespace RentaCar.Vehicles.Domain.Entities;
public sealed class Vehicle : Entity
{
    public string Brand { get; set; } = default!;
    public string Model { get; set; } = default!;
    public int Year { get; set; }
    public string Plate { get; set; } = default!;
    public decimal DailyPrice { get; set; }
    public string CoverImageUrl { get; set; } = default!;
    public FuelTypeEnum FuelType { get; set; } = FuelTypeEnum.Benzin;
    public GearTypeEnum GearType { get; set; } = GearTypeEnum.Manuel;
    public int DoorCount { get; set; }
    public int PassengerCapacity { get; set; }
}

public sealed class GearTypeEnum : SmartEnum<GearTypeEnum>
{
    public static GearTypeEnum Manuel = new("Manuel", 0);
    public static GearTypeEnum Otomatik = new("Otomatik", 1);
    public GearTypeEnum(string name, int value) : base(name, value)
    {
    }
}

public sealed class FuelTypeEnum : SmartEnum<FuelTypeEnum>
{
    public static FuelTypeEnum Benzin = new("Benzin", 0);
    public static FuelTypeEnum Dizel = new("Dizel", 1);
    public static FuelTypeEnum Elektrikli = new("Elektrikli", 2);
    public FuelTypeEnum(string name, int value) : base(name, value)
    {
    }
}