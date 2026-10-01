using Microsoft.EntityFrameworkCore;
using PAW.Models;

namespace PAW.Repositories;

public interface IUserActionRepository : IRepositoryBase<UserAction>
{
}

public class UserActionRepository : RepositoryBase<UserAction>, IUserActionRepository
{
    public override async Task<UserAction?> FindAsync(int id)
    {
        try
        {
            return await DbContext.Set<UserAction>().FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (Exception ex)
        {
            throw new PAWException(ex);
        }
    }

    public override async Task<bool> CreateAsync(UserAction entity)
    {
        try
        {
            var rows = await DbContext.Database.ExecuteSqlInterpolatedAsync(
                $"INSERT INTO UserActions (Id, Name, Description) VALUES ({entity.Id}, {entity.Name}, {entity.Description})");
            return rows > 0;
        }
        catch (Exception ex)
        {
            throw new PAWException(ex);
        }
    }

    public override async Task<bool> UpdateAsync(UserAction entity)
    {
        try
        {
            var rows = await DbContext.Database.ExecuteSqlInterpolatedAsync(
                $"UPDATE UserActions SET Name = {entity.Name}, Description = {entity.Description} WHERE Id = {entity.Id}");
            return rows > 0;
        }
        catch (Exception ex)
        {
            throw new PAWException(ex);
        }
    }

    public override async Task<bool> DeleteAsync(UserAction entity)
    {
        try
        {
            var rows = await DbContext.Database.ExecuteSqlInterpolatedAsync(
                $"DELETE FROM UserActions WHERE Id = {entity.Id}");
            return rows > 0;
        }
        catch (Exception ex)
        {
            throw new PAWException(ex);
        }
    }
}
