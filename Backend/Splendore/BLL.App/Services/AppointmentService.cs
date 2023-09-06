using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO;
using Contracts.Base;
using DAL.Contracts.App;

namespace BLL.App.Services;

public class AppointmentService : 
    BaseEntityService<BLL.DTO.Appointment, Domain.App.Appointment, IAppointmentRepository>, IAppointmentService
{
    protected IAppUOW Uow;

    public AppointmentService(IAppUOW uow, IMapper<BLL.DTO.Appointment, Domain.App.Appointment> mapper) 
        : base(uow.AppointmentRepository, mapper)
    {
        Uow = uow;
    }

    public async Task<IEnumerable<Appointment>> AllAsync(Guid userId)
    {
        return (await Uow.AppointmentRepository.AllAsync(userId)).Select(e => Mapper.Map(e));
    }

    public async Task<Appointment?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.AppointmentRepository.FindAsync(id, userId));
    }

    public async Task<Appointment?> RemoveAsync(Guid id, Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Appointment> AddAsync(Appointment entity)
    {
        return Mapper.Map(Repository.Add(Mapper.Map(entity)))!;
    }
}