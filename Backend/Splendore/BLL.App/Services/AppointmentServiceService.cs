using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;
using Domain.Base;

namespace BLL.App.Services;

public class AppointmentServiceService : 
    BaseEntityService<BLL.DTO.AppointmentService, Domain.App.AppointmentService, IAppointmentServiceRepository>, IAppointmentServiceService
{
    protected IAppUOW Uow;

    public AppointmentServiceService(IAppUOW uow, IMapper<BLL.DTO.AppointmentService, Domain.App.AppointmentService> mapper) 
        : base(uow.AppointmentServiceRepository, mapper)
    {
        Uow = uow;
    }

    public async Task<IEnumerable<DTO.AppointmentService>> AllByAppointmentIdAsync(Guid appointmentId)
    {
        return (await Repository.AllByAppointmentIdAsync(appointmentId)).Select(e => Mapper.Map(e));
    }
}