using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class AppointmentRepository : EFBaseRepository<Appointment, ApplicationDbContext>, IAppointmentRepository
{
    public AppointmentRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public override async Task<IEnumerable<Appointment>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(a => a.AppUser)
            .Include(a => a.AppointmentStatus)
            .Include(a => a.PaymentMethod)
            .Include(a => a.Stylist)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Appointment>> AllAsync(Guid userId)
    {
        return await RepositoryDbSet
            .Include(a => a.AppUser)
            .Include(a => a.AppointmentStatus)
            .Include(a => a.PaymentMethod)
            .Include(a => a.Stylist)
            .ThenInclude(s => s!.Salon)
            .Where(a => a.AppUserId == userId)
            .ToListAsync();
    }
    
    public override async Task<Appointment?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(a => a.AppUser)
            .Include(a => a.AppointmentStatus)
            .Include(a => a.PaymentMethod)
            .Include(a => a.Stylist)
            .ThenInclude(s => s!.Salon)
            .FirstOrDefaultAsync(a => a.Id == id);
    }
    
    public async Task<Appointment?> FindAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet
            .Include(a => a.AppUser)
            .Include(a => a.AppointmentStatus)
            .Include(a => a.PaymentMethod)
            .Include(a => a.Stylist)
            .ThenInclude(s => s!.Salon)
            .FirstOrDefaultAsync(a => a.Id == id && a.AppUserId == userId);
    }

    public async Task<Appointment> AddAsync(Appointment entity)
    {
        var addedAppointment = RepositoryDbSet.Add(entity).Entity;

        await RepositoryDbContext.SaveChangesAsync();
        
        return addedAppointment;
    }

    public async Task<Appointment?> RemoveAsync(Guid id, Guid userId)
    {
        var appointment = await FindAsync(id, userId);
        return appointment == null ? null : Remove(appointment);

    }
}