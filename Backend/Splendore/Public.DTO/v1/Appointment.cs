namespace Public.DTO.v1;

public class Appointment
{
    public Guid Id { get; set; }
    
    public DateTime Date { get; set; } = DateTime.Now; // mandatory
    
    public decimal TotalPrice { get; set; } // mandatory

    public Guid StylistId { get; set; } // mandatory
    // public Stylist? Stylist { get; set; }
    
    public Guid AppointmentStatusId { get; set; } // mandatory
    // public AppointmentStatus? AppointmentStatus { get; set; }

    public Guid ScheduleId { get; set; }
    
    public Guid? PaymentMethodId { get; set; }
    // public PaymentMethod? PaymentMethod { get; set; }
    
    // public ICollection<AppointmentService>? AppointmentServices { get; set; }
    
    public Guid AppUserId { get; set; } // should be assigned automatically in controller
    // public AppUser? AppUser { get; set; }

    public string? SalonId { get; set; }
    public string? SalonName { get; set; }
    public string? StylistName { get; set; }
    public string? AppointmentStatusName { get; set; }
}