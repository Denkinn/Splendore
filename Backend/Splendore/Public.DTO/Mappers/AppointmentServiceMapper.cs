using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class AppointmentServiceMapper : BaseMapper<BLL.DTO.AppointmentService, Public.DTO.v1.AppointmentService>
{
    public AppointmentServiceMapper(IMapper mapper) : base(mapper)
    {
    }
}