using Domain.Base;

namespace Domain.App.Identity;

public class AppRefreshToken : BaseRefreshToken
{

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
}