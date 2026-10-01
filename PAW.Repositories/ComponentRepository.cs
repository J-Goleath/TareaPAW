using PAW.Models;

namespace PAW.Repositories;

public interface IComponentRepository : IRepositoryBase<Component>
{
}

public class ComponentRepository : RepositoryBase<Component>, IComponentRepository
{
    public override async Task<Component?> FindAsync(int id)
    {
        try
        {
            return await DbContext.Set<Component>().FindAsync((decimal)id);
        }
        catch (Exception ex)
        {
            throw new PAWException(ex);
        }
    }
}
