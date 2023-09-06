using AutoMapper;
using DAL.Base;

namespace BLL.App.Mappers;

public class ServiceMapper : BaseMapper<BLL.DTO.Service, Domain.App.Service>
{
    public ServiceMapper(IMapper mapper) : base(mapper)
    {
    }
}