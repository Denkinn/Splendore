using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class AppointmentStatusMapper : BaseMapper<Domain.App.AppointmentStatus, Public.DTO.v1.AppointmentStatus>
{
    public AppointmentStatusMapper(IMapper mapper) : base(mapper)
    {
    }
}