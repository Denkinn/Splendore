using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;

namespace DAL.EF.App.Repositories;

public class ReviewRepository  : EFBaseRepository<Review, ApplicationDbContext>, IReviewRepository
{
    public ReviewRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public async Task<Review> AddAsync(Review entity)
    {
        var addedReview = RepositoryDbSet.Add(entity).Entity;

        await RepositoryDbContext.SaveChangesAsync();
        
        return addedReview;
    }
}