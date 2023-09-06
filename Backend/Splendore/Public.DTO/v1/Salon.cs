namespace Public.DTO.v1;

public class Salon
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;

    public int StylistCount { get; set; } = -1;
}