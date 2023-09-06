using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO;
using Contracts.Base;
using DAL.Contracts.App;

namespace BLL.App.Services;

public class ServiceService : 
    BaseEntityService<BLL.DTO.Service, Domain.App.Service, IServiceRepository>, IServiceService
{
    protected IAppUOW Uow;

    public ServiceService(IAppUOW uow, IMapper<BLL.DTO.Service, Domain.App.Service> mapper) 
        : base(uow.ServiceRepository, mapper)
    {
        Uow = uow;
    }
}