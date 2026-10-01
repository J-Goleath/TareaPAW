using PAW.Models;

namespace PAW.Repositories;

public interface ICategoryRepository : IRepositoryBase<Category>
{
}

public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
}
