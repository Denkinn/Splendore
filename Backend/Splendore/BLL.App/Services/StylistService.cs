using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO;
using Contracts.Base;
using DAL.Contracts.App;

namespace BLL.App.Services;

public class StylistService : 
    BaseEntityService<BLL.DTO.Stylist, Domain.App.Stylist, IStylistRepository>, IStylistService
{
    protected IAppUOW Uow;

    public StylistService(IAppUOW uow, IMapper<BLL.DTO.Stylist ,Domain.App.Stylist> mapper) 
        : base(uow.StylistRepository, mapper)
    {
        Uow = uow;
    }
    
    public async Task<IEnumerable<Stylist>> AllBySalonIdAsync(Guid salonId)
    {
        return (await Uow.StylistRepository.AllBySalonIdAsync(salonId)).Select(e => Mapper.Map(e));
    }
}