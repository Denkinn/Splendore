using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;

namespace BLL.App.Services;

public class SalonServiceService :
    BaseEntityService<BLL.DTO.SalonService, Domain.App.SalonService, ISalonServiceRepository>, ISalonServiceService
{
    protected IAppUOW Uow;

    public SalonServiceService(IAppUOW uow, IMapper<BLL.DTO.SalonService, Domain.App.SalonService> mapper) 
        : base(uow.SalonServiceRepository, mapper)
    {
        Uow = uow;
    }

    public async Task<IEnumerable<DTO.SalonService>> AllBySalonIdAsync(Guid id)
    {
        return (await Uow.SalonServiceRepository.AllBySalonIdAsync(id)).Select(e => Mapper.Map(e));
    }
}