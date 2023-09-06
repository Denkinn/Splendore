using AutoMapper;

namespace Public.DTO;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<BLL.DTO.Service, Public.DTO.v1.Service>().ReverseMap();

        CreateMap<BLL.DTO.Stylist, Public.DTO.v1.Stylist>().ReverseMap();

        CreateMap<BLL.DTO.Salon, Public.DTO.v1.Salon>().ReverseMap();

        CreateMap<BLL.DTO.Appointment, Public.DTO.v1.Appointment>().ReverseMap();
        
        CreateMap<BLL.DTO.SalonService, Public.DTO.v1.SalonService>().ReverseMap();

        CreateMap<BLL.DTO.AppointmentService, Public.DTO.v1.AppointmentService>().ReverseMap();
        
        CreateMap<BLL.DTO.Schedule, Public.DTO.v1.Schedule>().ReverseMap();

        CreateMap<BLL.DTO.Review, Public.DTO.v1.Review>().ReverseMap();
        
        // ===================================================================================

        CreateMap<Domain.App.AppointmentStatus, Public.DTO.v1.AppointmentStatus>().ReverseMap();
        
        CreateMap<Domain.App.PaymentMethod, Public.DTO.v1.PaymentMethod>().ReverseMap();
        
        CreateMap<Domain.App.ServiceType, Public.DTO.v1.ServiceType>().ReverseMap();
    }
}