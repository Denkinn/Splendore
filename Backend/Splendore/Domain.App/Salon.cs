using Domain.Base;

namespace Domain.App;

public class Salon : DomainEntityId
{
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;

    public ICollection<SalonService>? SalonServices { get; set; }
    public ICollection<Stylist>? Stylists { get; set; }
    public ICollection<Review>? Reviews { get; set; }
}