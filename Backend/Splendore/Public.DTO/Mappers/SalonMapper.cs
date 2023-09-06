using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class SalonMapper : BaseMapper<BLL.DTO.Salon, Public.DTO.v1.Salon>
{
    public SalonMapper(IMapper mapper) : base(mapper)
    {
    }

    public Public.DTO.v1.Salon? MapWithCount(DAL.DTO.SalonWithCount entity)
    {
        var res = Mapper.Map<Public.DTO.v1.Salon>(entity);
        return res;
    }
}