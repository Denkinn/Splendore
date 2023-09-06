using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface IAppointmentServiceRepository : IBaseRepository<AppointmentService>, IAppointmentServiceRepositoryCustom<AppointmentService> 
{
    
}

public interface IAppointmentServiceRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> AllByAppointmentIdAsync(Guid appointmentId);
}