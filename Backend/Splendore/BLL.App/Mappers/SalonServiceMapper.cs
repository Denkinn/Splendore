using AutoMapper;
using DAL.Base;

namespace BLL.App.Mappers;

public class SalonServiceMapper : BaseMapper<BLL.DTO.SalonService, Domain.App.SalonService>
{
    public SalonServiceMapper(IMapper mapper) : base(mapper)
    {
    }
}