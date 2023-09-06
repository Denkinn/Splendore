using AutoMapper;
using DAL.Base;

namespace BLL.App.Mappers;

public class AppointmentServiceMapper : BaseMapper<BLL.DTO.AppointmentService, Domain.App.AppointmentService>
{
    public AppointmentServiceMapper(IMapper mapper) : base(mapper)
    {
    }
}