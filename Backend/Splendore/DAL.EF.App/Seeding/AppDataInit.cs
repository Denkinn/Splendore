using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Seeding;

public static class AppDataInit
{
    private static Guid adminId = Guid.Parse("f7997b44-9077-46e4-ba25-bb8696257541");
    private static Guid userId = Guid.Parse("dfb401e2-fff1-4638-b785-320a936601f5");

    public static void MigrateDatabase(ApplicationDbContext context)
    {
        context.Database.Migrate();
    }

    public static void DropDatabase(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();
    }

    public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        (Guid id, string email, string pwd) adminData = (adminId, "admin@app.com", "Foobar.1");
        var admin = userManager.FindByEmailAsync(adminData.email).Result;

        if (admin == null)
        {
            admin = new AppUser()
            {
                Id = adminData.id,
                Email = adminData.email,
                UserName = adminData.email,
                FirstName = "Admin",
                LastName = "App",
                EmailConfirmed = true
            };
            var result = userManager.CreateAsync(admin, adminData.pwd).Result;
            if (!result.Succeeded)
            {
                throw new ApplicationException("Cannot seed admin user");
            }
        }

        // (Guid userId, string email, string pwd) userData = (userId, "user@test.ee", "Foo.bar1");
        // var user = userManager.FindByEmailAsync(userData.email).Result;
        //
        // if (user == null)
        // {
        //     user = new AppUser()
        //     {
        //         Id = userData.userId,
        //         Email = userData.email,
        //         UserName = userData.email,
        //         FirstName = "User",
        //         LastName = "Test",
        //         EmailConfirmed = true
        //     };
        //     var result = userManager.CreateAsync(user, userData.pwd).Result;
        //     if (!result.Succeeded)
        //     {
        //         throw new ApplicationException("Cannot seed user");
        //     }
        // }
    }

    public static void SeedAppData(ApplicationDbContext context)
    {
        SeedAppDataEntityTypes(context);
        SeedAppDataGeneral(context);

        context.SaveChanges();
    }

    private static void SeedAppDataEntityTypes(ApplicationDbContext context)
    {
        if (!context.ServiceTypes.Any())
        {
            context.ServiceTypes.Add(new ServiceType()
            {
                Name = "Haircut"
            });
            context.ServiceTypes.Add(new ServiceType()
            {
                Name = "Hair dyeing"
            });
        }

        if (!context.AppointmentStatuses.Any())
        {
            context.AppointmentStatuses.Add(new AppointmentStatus()
            {
                Name = "Active"
            });
            context.AppointmentStatuses.Add(new AppointmentStatus()
            {
                Name = "Over"
            });
            context.AppointmentStatuses.Add(new AppointmentStatus()
            {
                Name = "Cancelled"
            });
        }

        if (!context.PaymentMethods.Any())
        {
            context.PaymentMethods.Add(new PaymentMethod()
            {
                Name = "Debit card"
            });
            context.PaymentMethods.Add(new PaymentMethod()
            {
                Name = "Cash"
            });
        }
    }

    private static void SeedAppDataGeneral(ApplicationDbContext context)
    {
        // salon
        var salon = new Salon()
        {
            Name = "Salon Name",
            Address = "Kalamaja",
            Email = "salon@mail.com",
            PhoneNumber = "55472953"
        };
        if (!context.Salons.Any())
        {
            context.Salons.Add(salon);
        }

        // stylist
        // var stylist = new Stylist()
        // {
        //     Name = "Elizabeth",
        //     PhoneNumber = "789",
        //     AppUserId = Guid.Parse("dfb401e2-fff1-4638-b785-320a936601f5"),
        //     Salon = salon
        // };
        // if (!context.Stylists.Any())
        // {
        //     context.Stylists.Add(stylist);
        // }
        
        // service
        // var service = new Service()
        // {
        //     Name = "Basic men haircut",
        //     ServiceType = new ServiceType() {Name = "Haircut"}
        // };
        // if (!context.Services.Any())
        // {
        //     context.Add(service);
        // }
    }
}