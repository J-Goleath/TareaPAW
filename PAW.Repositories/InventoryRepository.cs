using PAW.Models;

namespace PAW.Repositories;

public interface IInventoryRepository : IRepositoryBase<Inventory>
{
}

public class InventoryRepository : RepositoryBase<Inventory>, IInventoryRepository
{
}
