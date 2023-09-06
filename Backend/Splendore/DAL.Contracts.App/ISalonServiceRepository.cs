using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface ISalonServiceRepository : IBaseRepository<SalonService>, ISalonServiceRepositoryCustom<SalonService>
{
    
}

public interface ISalonServiceRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> AllBySalonIdAsync(Guid id);
}