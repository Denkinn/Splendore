using DAL.Contracts.App;
using DAL.DTO;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class SalonRepository : EFBaseRepository<Salon, ApplicationDbContext>, ISalonRepository
{
    public SalonRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public override async Task<IEnumerable<Salon>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(s => s.Stylists)
            .ToListAsync();
    }

    public override async Task<Salon?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(s => s.Stylists)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
    
    // ==============================================================================================

    public async Task<IEnumerable<Salon>> AllAsync(Guid userId)
    {
        return await RepositoryDbSet
            .Include(s => s.Stylists)
            .ToListAsync();
    }

    public async Task<Salon?> FindAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet
            .Include(s => s.Stylists)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Salon?> RemoveAsync(Guid id, Guid userId)
    {
        throw new NotImplementedException();
    }
    

    // ====================================================================================================

    public async Task<IEnumerable<SalonWithCount>> AllWithCountAsync()
    {
        return await RepositoryDbSet
            .Include(s => s.Stylists)
            .Select(s => new SalonWithCount()
            {
                Id = s.Id,
                Name = s.Name,
                Address = s.Address,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                StylistCount = s.Stylists!.Count
            })
            .ToListAsync();
    }
}