namespace Public.DTO.v1;

public class Service
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = default!;

    public Guid ServiceTypeId { get; set; }
    // public ServiceType? ServiceType { get; set; }
    public string? ServiceType { get; set; }

    // public ICollection<SalonService>? SalonServices { get; set; }
}