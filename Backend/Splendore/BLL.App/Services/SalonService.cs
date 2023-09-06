using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO;
using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;

namespace BLL.App.Services;

public class SalonService : 
    BaseEntityService<BLL.DTO.Salon, Domain.App.Salon, ISalonRepository>, ISalonService
{
    protected IAppUOW Uow;

    public SalonService(IAppUOW uow, IMapper<BLL.DTO.Salon, Domain.App.Salon> mapper) 
        : base(uow.SalonRepository, mapper)
    {
        Uow = uow;
    }

    public async Task<IEnumerable<Salon>> AllAsync(Guid userId)
    {
        return (await Uow.SalonRepository.AllAsync(userId)).Select(e => Mapper.Map(e));
    }

    public async Task<Salon?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.SalonRepository.FindAsync(id, userId));
    }

    public async Task<Salon?> RemoveAsync(Guid id, Guid userId)
    {
        throw new NotImplementedException();
    }

    public Domain.App.SalonService AddService(Domain.App.SalonService service)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<SalonWithCount>> AllWithCountAsync()
    {
        throw new NotImplementedException();
    }
}