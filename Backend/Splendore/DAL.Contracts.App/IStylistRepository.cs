using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface IStylistRepository : IBaseRepository<Stylist>, IStylistRepositoryCustom<Stylist>
{
}

public interface IStylistRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> AllBySalonIdAsync(Guid salonId);
}
