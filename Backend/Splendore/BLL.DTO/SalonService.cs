using Domain.Base;

namespace BLL.DTO;

public class SalonService : DomainEntityId
{
    public decimal Price { get; set; }
    public int Time { get; set; }

    public Guid SalonId { get; set; }
    // public Salon? Salon { get; set; }
    public string? SalonName { get; set; }

    public Guid ServiceId { get; set; }
    // public Service? Service { get; set; }
    public string? ServiceName { get; set; }

    public string? ServiceType { get; set; }

    // public ICollection<AppointmentService>? AppointmentServices { get; set; }
}