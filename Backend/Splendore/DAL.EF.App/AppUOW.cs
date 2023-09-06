using DAL.Contracts.App;
using DAL.EF.App.Repositories;
using DAL.EF.Base;

namespace DAL.EF.App;

public class AppUOW : EFBaseUOW<ApplicationDbContext>, IAppUOW
{
    public AppUOW(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    private IAppointmentRepository? _appointmentRepository;
    public IAppointmentRepository AppointmentRepository =>
        _appointmentRepository ??= new AppointmentRepository(UowDbContext);
    
    private IServiceRepository? _serviceRepository;
    public IServiceRepository ServiceRepository =>
        _serviceRepository ??= new ServiceRepository(UowDbContext);
    
    private IServiceTypeRepository? _serviceTypeRepository;
    public IServiceTypeRepository ServiceTypeRepository =>
        _serviceTypeRepository ??= new ServiceTypeRepository(UowDbContext);

    private IStylistRepository? _stylistRepository;
    public IStylistRepository StylistRepository =>
        _stylistRepository ??= new StylistRepository(UowDbContext);

    private ISalonRepository? _salonRepository;
    public ISalonRepository SalonRepository =>
        _salonRepository ??= new SalonRepository(UowDbContext);
    
    private ISalonServiceRepository? _salonServiceRepository;
    public ISalonServiceRepository SalonServiceRepository =>
        _salonServiceRepository ??= new SalonServiceRepository(UowDbContext);
    
    private IAppointmentServiceRepository? _appointmentServiceRepository;
    public IAppointmentServiceRepository AppointmentServiceRepository =>
        _appointmentServiceRepository ??= new AppointmentServiceRepository(UowDbContext);
    
    private IScheduleRepository? _scheduleRepository;
    public IScheduleRepository ScheduleRepository =>
        _scheduleRepository ??= new ScheduleRepository(UowDbContext);
    
    private IReviewRepository? _reviewRepository;
    public IReviewRepository ReviewRepository =>
        _reviewRepository ??= new ReviewRepository(UowDbContext);
    
}