using Microsoft.EntityFrameworkCore;
using PAW.Models;

namespace PAW.Repositories;

public interface IUserRoleRepository : IRepositoryBase<UserRole>
{
}

public class UserRoleRepository : RepositoryBase<UserRole>, IUserRoleRepository
{
    public override async Task<UserRole?> FindAsync(int id)
    {
        try
        {
            return await DbContext.Set<UserRole>().FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (Exception ex)
        {
            throw new PAWException(ex);
        }
    }

    public override async Task<bool> CreateAsync(UserRole entity)
    {
        try
        {
            var rows = await DbContext.Database.ExecuteSqlInterpolatedAsync(
                $"INSERT INTO UserRoles (Id, RoldID, UserID) VALUES ({entity.Id}, {entity.RoldId}, {entity.UserId})");
            return rows > 0;
        }
        catch (Exception ex)
        {
            throw new PAWException(ex);
        }
    }

    public override async Task<bool> UpdateAsync(UserRole entity)
    {
        try
        {
            var rows = await DbContext.Database.ExecuteSqlInterpolatedAsync(
                $"UPDATE UserRoles SET RoldID = {entity.RoldId}, UserID = {entity.UserId} WHERE Id = {entity.Id}");
            return rows > 0;
        }
        catch (Exception ex)
        {
            throw new PAWException(ex);
        }
    }

    public override async Task<bool> DeleteAsync(UserRole entity)
    {
        try
        {
            var rows = await DbContext.Database.ExecuteSqlInterpolatedAsync(
                $"DELETE FROM UserRoles WHERE Id = {entity.Id}");
            return rows > 0;
        }
        catch (Exception ex)
        {
            throw new PAWException(ex);
        }
    }
}
