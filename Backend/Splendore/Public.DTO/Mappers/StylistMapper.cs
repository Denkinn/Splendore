using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class StylistMapper : BaseMapper<BLL.DTO.Stylist, DTO.v1.Stylist>
{
    public StylistMapper(IMapper mapper) : base(mapper)
    {
    }
}