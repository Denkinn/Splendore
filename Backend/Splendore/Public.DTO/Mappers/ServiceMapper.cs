using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class ServiceMapper : BaseMapper<BLL.DTO.Service, Public.DTO.v1.Service>
{
    public ServiceMapper(IMapper mapper) : base(mapper)
    {
    }
}