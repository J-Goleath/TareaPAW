using PAW.Models;

namespace PAW.Repositories;

public interface ISupplierRepository : IRepositoryBase<Supplier>
{
}

public class SupplierRepository : RepositoryBase<Supplier>, ISupplierRepository
{
}
