using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO;
using Contracts.Base;
using DAL.Contracts.App;

namespace BLL.App.Services;

public class ReviewService : 
    BaseEntityService<BLL.DTO.Review, Domain.App.Review, IReviewRepository>, IReviewService
{
    protected IAppUOW Uow;

    public ReviewService(IAppUOW uow, IMapper<BLL.DTO.Review, Domain.App.Review> mapper) 
        : base(uow.ReviewRepository, mapper)
    {
        Uow = uow;
    }

    public async Task<Review> AddAsync(Review entity)
    {
        return Mapper.Map(Repository.Add(Mapper.Map(entity)))!;
    }
}