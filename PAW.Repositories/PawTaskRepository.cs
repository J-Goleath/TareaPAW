using PAW.Models;

namespace PAW.Repositories;

public interface IPawTaskRepository : IRepositoryBase<PawTask>
{
}

public class PawTaskRepository : RepositoryBase<PawTask>, IPawTaskRepository
{
}
