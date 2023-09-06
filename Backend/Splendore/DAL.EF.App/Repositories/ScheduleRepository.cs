using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class ScheduleRepository : EFBaseRepository<Schedule, ApplicationDbContext>, IScheduleRepository
{
    public ScheduleRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public async Task<IEnumerable<Schedule>> AllByStylistIdAsync(Guid stylistId)
    {
        return await RepositoryDbSet
            .Include(s => s.Stylist)
            .Where(s => s.Date > DateTime.Now && s.StylistId == stylistId)
            .OrderBy(s => s.Date)
            .ToListAsync();
    }
}