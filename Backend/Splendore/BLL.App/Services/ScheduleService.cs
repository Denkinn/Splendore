using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO;
using Contracts.Base;
using DAL.Contracts.App;

namespace BLL.App.Services;

public class ScheduleService : 
    BaseEntityService<BLL.DTO.Schedule, Domain.App.Schedule, IScheduleRepository>, IScheduleService
{
    protected IAppUOW Uow;

    public ScheduleService(IAppUOW uow, IMapper<BLL.DTO.Schedule, Domain.App.Schedule> mapper) 
        : base(uow.ScheduleRepository, mapper)
    {
        Uow = uow;
    }

    public async Task<IEnumerable<Schedule>> AllByStylistIdAsync(Guid stylistId)
    {
        return (await Repository.AllByStylistIdAsync(stylistId)).Select(e => Mapper.Map(e));
    }
}