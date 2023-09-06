using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class StylistRepository : EFBaseRepository<Stylist, ApplicationDbContext>, IStylistRepository
{
    public StylistRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }


    public override async Task<IEnumerable<Stylist>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(s => s.AppUser)
            .Include(s => s.Salon)
            .ToListAsync();
    }

    public override async Task<Stylist?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(s => s.AppUser)
            .Include(s => s.Salon)
            .Include(s => s.Schedules)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
    
    public async Task<IEnumerable<Stylist>> AllBySalonIdAsync(Guid salonId)
    {
        return await RepositoryDbSet
            .Include(s => s.AppUser)
            .Include(s => s.Salon)
            .Include(s => s.Schedules)
            .Where(s => s.SalonId == salonId)
            .ToListAsync();

    }
    
}