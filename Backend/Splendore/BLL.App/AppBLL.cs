using AutoMapper;
using BLL.App.Mappers;
using BLL.App.Services;
using BLL.Base;
using BLL.Contracts.App;
using DAL.Contracts.App;

namespace BLL.App;

public class AppBLL : BaseBLL<IAppUOW>, IAppBLL
{
    protected IAppUOW Uow;
    private readonly AutoMapper.IMapper _mapper;
    
    public AppBLL(IAppUOW uow, IMapper mapper) : base(uow)
    {
        Uow = uow;
        _mapper = mapper;
    }

    private ISalonService? _salonService;
    public ISalonService SalonService =>
        _salonService ??= new SalonService(Uow, new SalonMapper(_mapper));

    private IStylistService? _stylistService;
    public IStylistService StylistService =>
        _stylistService ??= new StylistService(Uow, new StylistMapper(_mapper));
    
    private IAppointmentService? _appointmentService;
    public IAppointmentService AppointmentService =>
        _appointmentService ??= new AppointmentService(Uow, new AppointmentMapper(_mapper));
    
    private IServiceService? _serviceService;
    public IServiceService ServiceService =>
        _serviceService ??= new ServiceService(Uow, new ServiceMapper(_mapper));
    
    private ISalonServiceService? _salonServiceService;
    public ISalonServiceService SalonServiceService =>
        _salonServiceService ??= new SalonServiceService(Uow, new SalonServiceMapper(_mapper));
    
    private IAppointmentServiceService? _appointmentServiceService;
    public IAppointmentServiceService AppointmentServiceService =>
        _appointmentServiceService ??= new AppointmentServiceService(Uow, new AppointmentServiceMapper(_mapper));
    
    private IScheduleService? _scheduleService;
    public IScheduleService ScheduleService =>
        _scheduleService ??= new ScheduleService(Uow, new ScheduleMapper(_mapper));
    
    private IReviewService? _reviewService;
    public IReviewService ReviewService =>
        _reviewService ??= new ReviewService(Uow, new ReviewMapper(_mapper));
    
}