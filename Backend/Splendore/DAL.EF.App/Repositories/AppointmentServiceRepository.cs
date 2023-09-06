using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class AppointmentServiceRepository : EFBaseRepository<AppointmentService, ApplicationDbContext>, IAppointmentServiceRepository
{
    public AppointmentServiceRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public override async Task<IEnumerable<AppointmentService>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(a => a.SalonService)
            .ThenInclude(s => s!.Service!.ServiceType)
            .ToListAsync();
    }

    public async Task<IEnumerable<AppointmentService>> AllByAppointmentIdAsync(Guid appointmentId)
    {
        return await RepositoryDbSet
            .Include(a => a.SalonService)
            .ThenInclude(s => s!.Service!.ServiceType)
            .Where(a => a.AppointmentId == appointmentId)
            .ToListAsync();
    }
}