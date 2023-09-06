using Domain.Base;

namespace Domain.App;

public class AppointmentStatus : DomainEntityId
{
    public string Name { get; set; } = default!;

    public ICollection<Appointment>? Appointments { get; set; }
}