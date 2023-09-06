using AutoMapper;
using DAL.Base;

namespace BLL.App.Mappers;

public class StylistMapper: BaseMapper<BLL.DTO.Stylist, Domain.App.Stylist>
{
    public StylistMapper(IMapper mapper) : base(mapper)
    {
    }
}