using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class ServiceRepository : EFBaseRepository<Service, ApplicationDbContext>, IServiceRepository
{
    public ServiceRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
    
    public override async Task<IEnumerable<Service>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(s => s.ServiceType)
            .ToListAsync();
    }
    
    public override async Task<Service?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(s => s.ServiceType)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}