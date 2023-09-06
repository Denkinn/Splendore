using AutoMapper;
using DAL.Base;

namespace BLL.App.Mappers;

public class AppointmentMapper : BaseMapper<BLL.DTO.Appointment, Domain.App.Appointment>
{
    public AppointmentMapper(IMapper mapper) : base(mapper)
    {
    }
}