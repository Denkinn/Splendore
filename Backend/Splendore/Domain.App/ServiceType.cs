using Domain.Base;

namespace Domain.App;

public class ServiceType : DomainEntityId
{
    public string Name { get; set; } = default!;

    public ICollection<Service>? Services { get; set; }
}