namespace DAL.DTO;

public class Appointment
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; } = default!;
    public DateTime? PaymentDate { get; set; } = default!;
    public decimal TotalPrice { get; set; }
}