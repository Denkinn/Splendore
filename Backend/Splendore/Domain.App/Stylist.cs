using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class Stylist : DomainEntityId
{
    public string Name { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;

    public Guid SalonId { get; set; }
    public Salon? Salon { get; set; }

    public ICollection<Schedule>? Schedules { get; set; }
    public ICollection<Appointment>? Appointments { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}