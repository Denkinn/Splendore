using Domain.Base;

namespace Domain.App;

public class SalonService : DomainEntityId
{
    public decimal Price { get; set; }
    public int Time { get; set; }

    public Guid SalonId { get; set; }
    public Salon? Salon { get; set; }

    public Guid ServiceId { get; set; }
    public Service? Service { get; set; }

    public ICollection<AppointmentService>? AppointmentServices { get; set; }
}