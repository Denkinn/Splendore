using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class SalonServiceMapper : BaseMapper<BLL.DTO.SalonService, Public.DTO.v1.SalonService>
{
    public SalonServiceMapper(IMapper mapper) : base(mapper)
    {
    }
}