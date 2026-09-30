using APW.Architecture;
using PAW.Architecture.Providers;
using PAW.Models.DTO;

namespace TareaPAW.Services
{
    public interface IUserRoleService
    {
        Task<IEnumerable<UserRoleDTO>> GetUserRolesAsync();
        Task<UserRoleDTO?> GetUserRoleAsync(int id);
        Task<bool> CreateUserRoleAsync(UserRoleDTO dto);
        Task<bool> UpdateUserRoleAsync(int id, UserRoleDTO dto);
        Task<bool> DeleteUserRoleAsync(int id);
    }

    public class UserRoleService : ServiceBase, IUserRoleService
    {
        private readonly IRestProvider _restProvider;

        public UserRoleService(IRestProvider restProvider, IConfiguration configuration)
            : base(configuration, "UserRoles")
        {
            _restProvider = restProvider;
        }

        public async Task<IEnumerable<UserRoleDTO>> GetUserRolesAsync()
        {
            var response = await _restProvider.GetAsync(Endpoint, null);
            var items = JsonProvider.DeserializeSimple<IEnumerable<UserRoleDTO>>(response);
            return items ?? new List<UserRoleDTO>();
        }

        public async Task<UserRoleDTO?> GetUserRoleAsync(int id)
        {
            var response = await _restProvider.GetAsync(Endpoint, id.ToString());
            return JsonProvider.DeserializeSimple<UserRoleDTO>(response);
        }

        public async Task<bool> CreateUserRoleAsync(UserRoleDTO dto)
        {
            try
            {
                await _restProvider.PostAsync(Endpoint, JsonProvider.Serialize(dto));
                return true;
            }
            catch (ApplicationException)
            {
                return false;
            }
        }

        public async Task<bool> UpdateUserRoleAsync(int id, UserRoleDTO dto)
        {
            try
            {
                await _restProvider.PutAsync(Endpoint, id.ToString(), JsonProvider.Serialize(dto));
                return true;
            }
            catch (ApplicationException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserRoleAsync(int id)
        {
            try
            {
                await _restProvider.DeleteAsync(Endpoint, id.ToString());
                return true;
            }
            catch (ApplicationException)
            {
                return false;
            }
        }
    }
}
