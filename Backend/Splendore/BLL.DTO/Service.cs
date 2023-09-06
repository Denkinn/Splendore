using Domain.Base;

namespace BLL.DTO;

public class Service : DomainEntityId
{
    public string Name { get; set; } = default!;

    public Guid ServiceTypeId { get; set; }
    // public ServiceType? ServiceType { get; set; }
    public string? ServiceType { get; set; }

    // public ICollection<SalonService>? SalonServices { get; set; }
}