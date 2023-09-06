using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface ISalonService : IBaseRepository<BLL.DTO.Salon>, ISalonRepositoryCustom<BLL.DTO.Salon> 
{
    // add custom service methods here
}