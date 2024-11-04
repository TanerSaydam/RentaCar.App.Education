namespace RentaCar.Vehicles.Domain.Abstractions;
public abstract class Entity
{
    public Guid Id { get; set; }
    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    public DateTime CreatedDate { get; set; }
    public Guid CreatedUserId { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public Guid? UpdatedUserId { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedDate { get; set; }
    public Guid? DeletedUserId { get; set; }
    public bool IsActive { get; set; } = true;
}
