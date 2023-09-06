namespace Public.DTO.v1;

public class SalonWithLists
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;

    public ICollection<Stylist> Stylists { get; set; } = default!;
}