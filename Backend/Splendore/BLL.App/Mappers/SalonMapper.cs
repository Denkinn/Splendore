using AutoMapper;
using DAL.Base;

namespace BLL.App.Mappers;

public class SalonMapper : BaseMapper<BLL.DTO.Salon, Domain.App.Salon>
{
    public SalonMapper(IMapper mapper) : base(mapper)
    {
    }
}