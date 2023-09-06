namespace Public.DTO.v1;

public class Review
{
    public Guid Id { get; set; }
    
    public string? Commentary { get; set; }
    public ERating Rating { get; set; }

    public Guid SalonId { get; set; }
    // public Salon? Salon { get; set; }
}