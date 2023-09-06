using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class SalonServiceRepository : EFBaseRepository<SalonService, ApplicationDbContext>, ISalonServiceRepository
{
    public SalonServiceRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
    
    public override async Task<IEnumerable<SalonService>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(ss => ss.Salon)
            .Include(ss => ss.Service)
            .ThenInclude(st => st!.ServiceType)
            .ToListAsync();
    }
    
    public override async Task<SalonService?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(ss => ss.Salon)
            .Include(ss => ss.Service)
            .ThenInclude(st => st!.ServiceType)
            .FirstOrDefaultAsync(ss => ss.Id == id);
    }

    public async Task<IEnumerable<SalonService>> AllBySalonIdAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(ss => ss.Salon)
            .Include(ss => ss.Service)
            .ThenInclude(st => st!.ServiceType)
            .Where(ss => ss.SalonId == id)
            .ToListAsync();
    }
}