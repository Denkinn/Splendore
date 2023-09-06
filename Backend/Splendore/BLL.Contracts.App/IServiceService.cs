using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IServiceService : IBaseRepository<BLL.DTO.Service>, IServiceRepositoryCustom<BLL.DTO.Service>
{
    
}