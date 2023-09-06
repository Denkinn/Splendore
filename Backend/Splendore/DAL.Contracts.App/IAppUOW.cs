using DAL.Contracts.Base;

namespace DAL.Contracts.App;

public interface IAppUOW : IBaseUOW
{
    // list your repositories here
    IServiceTypeRepository ServiceTypeRepository { get; }
    IServiceRepository ServiceRepository { get; }
    IStylistRepository StylistRepository { get; }
    IAppointmentRepository AppointmentRepository { get; }
    ISalonRepository SalonRepository { get; }
    IAppointmentServiceRepository AppointmentServiceRepository { get; }
    ISalonServiceRepository SalonServiceRepository { get; }
    IScheduleRepository ScheduleRepository { get; }
    IReviewRepository ReviewRepository { get; }
}