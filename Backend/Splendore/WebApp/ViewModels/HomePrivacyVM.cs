using System.Collections.ObjectModel;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
#pragma warning disable CS1591
namespace WebApp.Models;

public class HomePrivacyVM
{
    public AppUser? AppUser { get; set; }
    public ICollection<IdentityUserClaim<Guid>>? AppUserClaims { get; set; }
}