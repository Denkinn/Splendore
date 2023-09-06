using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class Appointment : DomainEntityId
{
    public DateTime Date { get; set; } = DateTime.Now;
    
    public decimal TotalPrice { get; set; }
    
    public Guid StylistId { get; set; }
    public Stylist? Stylist { get; set; }

    public Guid AppointmentStatusId { get; set; }
    public AppointmentStatus? AppointmentStatus { get; set; }

    public Guid ScheduleId { get; set; }
    // public DateTime? PaymentDate { get; set; }
    
    public Guid? PaymentMethodId { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }

    public ICollection<AppointmentService>? AppointmentServices { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}