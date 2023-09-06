using Domain.Base;

namespace Domain.App;

public class PaymentMethod : DomainEntityId
{
    public string Name { get; set; } = default!;

    public ICollection<Appointment>? Appointments { get; set; }
}