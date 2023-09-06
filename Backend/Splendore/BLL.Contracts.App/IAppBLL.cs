using BLL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IAppBLL : IBaseBLL
{
    ISalonService SalonService { get; }
    IStylistService StylistService { get; }
    IAppointmentService AppointmentService { get; }
    IServiceService ServiceService { get; }
    ISalonServiceService SalonServiceService { get; }
    IAppointmentServiceService AppointmentServiceService { get; }
    IScheduleService ScheduleService { get; }
    IReviewService ReviewService { get; }
}