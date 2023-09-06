using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class ServiceTypeRepository : EFBaseRepository<ServiceType, ApplicationDbContext>, IServiceTypeRepository
{
    public ServiceTypeRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    // just example, is not needed here
    public override async Task<IEnumerable<ServiceType>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(st => st.Services)
            .OrderBy(st => st.Name)
            .ToListAsync();
    }

    // just example, is not needed here
    public override async Task<ServiceType?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(st => st.Services)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
}