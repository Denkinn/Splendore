using DAL.Contracts.Base;
using Domain.Contracts.Base;

namespace BLL.Contracts.Base;

/// <summary>
/// Service connected to some domain entity model.
/// </summary>
public interface IEntityService<TEntity> : IBaseRepository<TEntity>, IEntityService<TEntity, Guid>
    where TEntity : class, IDomainEntityId
{
    
}

public interface IEntityService<TEntity, Tkey> : IBaseRepository<TEntity, Tkey>
    where TEntity : class, IDomainEntityId<Tkey>
    where Tkey : struct, IEquatable<Tkey>
{
    
}