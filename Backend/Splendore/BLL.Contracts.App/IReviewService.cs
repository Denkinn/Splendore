using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IReviewService : IBaseRepository<BLL.DTO.Review>, IReviewRepositoryCustom<BLL.DTO.Review> 
{
    
}