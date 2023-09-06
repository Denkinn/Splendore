using Domain.Base;

namespace Domain.App;

public class Schedule : DomainEntityId
{
    public DateTime Date { get; set; }
    
    public bool IsBusy { get; set; }

    public Guid StylistId { get; set; }
    public Stylist? Stylist { get; set; }
}