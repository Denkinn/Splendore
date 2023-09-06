using Domain.Base;

namespace Domain.App;

public class AppointmentService : DomainEntityId
{
    public Guid SalonServiceId { get; set; }
    public SalonService? SalonService { get; set; }

    public Guid AppointmentId { get; set; }
    public Appointment? Appointment { get; set; }
}