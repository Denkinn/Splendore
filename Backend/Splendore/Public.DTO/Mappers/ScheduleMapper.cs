using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class ScheduleMapper : BaseMapper<BLL.DTO.Schedule, Public.DTO.v1.Schedule>
{
    public ScheduleMapper(IMapper mapper) : base(mapper)
    {
    }
}