using AutoMapper;
using DAL.Base;

namespace BLL.App.Mappers;

public class ReviewMapper : BaseMapper<BLL.DTO.Review, Domain.App.Review>
{
    public ReviewMapper(IMapper mapper) : base(mapper)
    {
    }
}