using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface IAppointmentRepository : IBaseRepository<Appointment>, IAppointmentRepositoryCustom<Appointment>
{
}

public interface IAppointmentRepositoryCustom<TEntity>
{
    public Task<IEnumerable<TEntity>> AllAsync(Guid userId);
    public Task<TEntity?> FindAsync(Guid id, Guid userId);
    Task<TEntity?> RemoveAsync(Guid id, Guid userId);
    Task<TEntity> AddAsync(TEntity entity);
}