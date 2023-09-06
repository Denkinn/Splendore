using DAL.Contracts.App;
using DAL.Contracts.Base;
namespace BLL.Contracts.App;

public interface IAppointmentServiceService : IBaseRepository<BLL.DTO.AppointmentService>, IAppointmentServiceRepositoryCustom<BLL.DTO.AppointmentService>
{
    
}