using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface IServiceRepository : IBaseRepository<Service>, IServiceRepositoryCustom<Service>
{
}

public interface IServiceRepositoryCustom<TEntity>
{
    // todo: remove
    // Task<IEnumerable<SalonService>> AllSalonServicesAsync();
    // Task<SalonService?> FindSalonServiceAsync(Guid id);
    // SalonService UpdateSalonService(SalonService salonService);
    // SalonService AddSalonService(SalonService salonService);
    // SalonService RemoveSalonService(SalonService salonService);
    // Task<SalonService?> RemoveSalonServiceAsync(Guid id);
}