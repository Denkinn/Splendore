using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class ServiceTypeMapper : BaseMapper<Domain.App.ServiceType, Public.DTO.v1.ServiceType>
{
    public ServiceTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}