using Domain.Base;

namespace BLL.DTO;

public class Schedule : DomainEntityId
{
    public DateTime Date { get; set; }
    
    public bool IsBusy { get; set; }

    public Guid StylistId { get; set; }
    // public Stylist? Stylist { get; set; }
    
}