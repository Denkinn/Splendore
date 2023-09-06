using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IAppointmentService : IBaseRepository<BLL.DTO.Appointment>, IAppointmentRepositoryCustom<BLL.DTO.Appointment>
{
    
}