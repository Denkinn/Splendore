using AutoMapper;

namespace BLL.App;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<BLL.DTO.Salon, Domain.App.Salon>().ReverseMap();
        
        CreateMap<BLL.DTO.Stylist, Domain.App.Stylist>().ReverseMap();

        CreateMap<Domain.App.Appointment, BLL.DTO.Appointment>()
            .ForMember(dest => dest.SalonName,
                options =>
                    options.MapFrom(src => src.Stylist!.Salon!.Name))
            .ForMember(dest => dest.AppointmentStatusName,
                options =>
                    options.MapFrom(src => src.AppointmentStatus!.Name))
            .ForMember(dest => dest.SalonId,
                options =>
                    options.MapFrom(src => src.Stylist!.SalonId));
        CreateMap<BLL.DTO.Appointment, Domain.App.Appointment>();

        CreateMap<Domain.App.Service, BLL.DTO.Service>()
            .ForMember(dest => dest.ServiceType,
                options =>
                    options.MapFrom(src => src.ServiceType!.Name));
        CreateMap<BLL.DTO.Service, Domain.App.Service>();

        CreateMap<Domain.App.SalonService, BLL.DTO.SalonService>()
            .ForMember(dest => dest.ServiceType,
                options =>
                    options.MapFrom(src => src.Service!.ServiceType!.Name));
        CreateMap<BLL.DTO.SalonService, Domain.App.SalonService>();

        CreateMap<Domain.App.AppointmentService, BLL.DTO.AppointmentService>()
            .ForMember(dest => dest.Price,
                options =>
                    options.MapFrom(src => src.SalonService!.Price))
            .ForMember(dest => dest.Time,
                options =>
                    options.MapFrom(src => src.SalonService!.Time))
            .ForMember(dest => dest.ServiceName,
                options =>
                    options.MapFrom(src => src.SalonService!.Service!.Name))
            .ForMember(dest => dest.ServiceType,
                options =>
                    options.MapFrom(src => src.SalonService!.Service!.ServiceType!.Name));
        CreateMap<BLL.DTO.AppointmentService, Domain.App.AppointmentService>();
        
        CreateMap<BLL.DTO.Schedule, Domain.App.Schedule>().ReverseMap();

        CreateMap<BLL.DTO.Review, Domain.App.Review>().ReverseMap();
    }
}