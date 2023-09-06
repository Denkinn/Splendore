using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IScheduleService : IBaseRepository<BLL.DTO.Schedule>, IScheduleRepositoryCustom<BLL.DTO.Schedule>
{
    
}