namespace Public.DTO.v1;

public class Schedule
{
    public Guid Id { get; set; }
    
    public DateTime Date { get; set; }
    
    public bool IsBusy { get; set; }

    public Guid StylistId { get; set; }
    // public Stylist? Stylist { get; set; }
}