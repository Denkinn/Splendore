using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface ISalonServiceService : IBaseRepository<BLL.DTO.SalonService>, ISalonServiceRepositoryCustom<BLL.DTO.SalonService>
{
    
}