using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class ReviewMapper : BaseMapper<BLL.DTO.Review, Public.DTO.v1.Review>
{
    public ReviewMapper(IMapper mapper) : base(mapper)
    {
    }
}