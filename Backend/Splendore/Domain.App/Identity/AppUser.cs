using System.ComponentModel.DataAnnotations;
using Domain.Contracts.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity;

public class AppUser : IdentityUser<Guid>, IDomainEntityId
{
    [MaxLength(128)]
    public string FirstName { get; set; } = default!;

    [MaxLength(128)]
    public string LastName { get; set; } = default!;
    
    public ICollection<Appointment>? Appointments { get; set; }
    
    public Stylist? Stylist { get; set; }
    
    
    public ICollection<AppRefreshToken>? AppRefreshTokens { get; set; }
}