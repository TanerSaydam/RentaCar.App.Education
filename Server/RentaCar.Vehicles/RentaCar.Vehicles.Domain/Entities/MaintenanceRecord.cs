using RentaCar.Vehicles.Domain.Abstractions;

namespace RentaCar.Vehicles.Domain.Entities;
public sealed class MaintenanceRecord : Entity
{
    public DateTime Date { get; set; }
    public string Description { get; set; } = default!;

}
