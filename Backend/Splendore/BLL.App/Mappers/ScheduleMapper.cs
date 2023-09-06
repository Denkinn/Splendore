using AutoMapper;
using DAL.Base;

namespace BLL.App.Mappers;

public class ScheduleMapper : BaseMapper<BLL.DTO.Schedule, Domain.App.Schedule>
{
    public ScheduleMapper(IMapper mapper) : base(mapper)
    {
    }
}