using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface IScheduleRepository : IBaseRepository<Schedule>, IScheduleRepositoryCustom<Schedule>
{
    
}

public interface IScheduleRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> AllByStylistIdAsync(Guid stylistId);
}