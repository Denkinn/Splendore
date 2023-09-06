using Domain.Base;

namespace Public.DTO.v1;

public class AppointmentService
{
    public Guid Id { get; set; }
    
    public Guid SalonServiceId { get; set; }
    // public SalonService? SalonService { get; set; }

    public Guid AppointmentId { get; set; }
    // public Appointment? Appointment { get; set; }

    public decimal? Price { get; set; }
    public int? Time { get; set; }
    public string? ServiceName { get; set; }
    public string? ServiceType { get; set; }
}