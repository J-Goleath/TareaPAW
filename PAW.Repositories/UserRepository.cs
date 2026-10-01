using PAW.Models;

namespace PAW.Repositories;

public interface IUserRepository : IRepositoryBase<User>
{
}

public class UserRepository : RepositoryBase<User>, IUserRepository
{
}
