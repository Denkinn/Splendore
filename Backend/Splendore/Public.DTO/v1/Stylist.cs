namespace Public.DTO.v1;

public class Stylist
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;

    public Guid SalonId { get; set; }
    // public Salon? Salon { get; set; }
    //
    // public ICollection<Schedule>? Schedules { get; set; }
    // public ICollection<Appointment>? Appointments { get; set; }
    //
    public Guid AppUserId { get; set; }
    // public AppUser? AppUser { get; set; }
}