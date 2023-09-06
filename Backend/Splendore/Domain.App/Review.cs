using Domain.Base;

namespace Domain.App;

public class Review : DomainEntityId
{
    public string? Commentary { get; set; }
    public ERating Rating { get; set; }

    public Guid SalonId { get; set; }
    public Salon? Salon { get; set; }
}