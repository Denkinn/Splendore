using DAL.Contracts.Base;
using DAL.DTO;
using Domain.App;

namespace DAL.Contracts.App;

public interface ISalonRepository : IBaseRepository<Salon>, ISalonRepositoryCustom<Salon>
{
    // custom methods for repo only
}

public interface ISalonRepositoryCustom<TEntity>
{
    // shared methods between repo and service
    
    Task<IEnumerable<TEntity>> AllAsync(Guid userId);

    Task<TEntity?> FindAsync(Guid id, Guid userId);

    Task<TEntity?> RemoveAsync(Guid id, Guid userId);

    // Task<SalonService?> AddService(Guid salonId, Guid userId, SalonService salonService);

}