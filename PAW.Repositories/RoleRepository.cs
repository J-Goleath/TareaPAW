using PAW.Models;

namespace PAW.Repositories;

public interface IRoleRepository : IRepositoryBase<Role>
{
}

public class RoleRepository : RepositoryBase<Role>, IRoleRepository
{
}
