using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface IReviewRepository : IBaseRepository<Review>, IReviewRepositoryCustom<Review>
{
    
}

public interface IReviewRepositoryCustom<TEntity>
{
    Task<TEntity> AddAsync(TEntity entity);
}