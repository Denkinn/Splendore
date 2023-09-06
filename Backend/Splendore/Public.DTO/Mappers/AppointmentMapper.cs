using AutoMapper;
using DAL.Base;
using Domain.App;

namespace Public.DTO.Mappers;

public class AppointmentMapper : BaseMapper<BLL.DTO.Appointment, Public.DTO.v1.Appointment>
{
    public AppointmentMapper(IMapper mapper) : base(mapper)
    {
    }
}