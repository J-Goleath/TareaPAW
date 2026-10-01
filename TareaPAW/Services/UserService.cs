using APW.Architecture;
using PAW.Architecture.Providers;
using PAW.Models.DTO;

namespace TareaPAW.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetUsersAsync();
        Task<UserDTO?> GetUserAsync(int id);
        Task<bool> CreateUserAsync(UserDTO dto);
        Task<bool> UpdateUserAsync(int id, UserDTO dto);
        Task<bool> DeleteUserAsync(int id);
    }

    public class UserService : ServiceBase, IUserService
    {
        private readonly IRestProvider _restProvider;

        public UserService(IRestProvider restProvider, IConfiguration configuration)
            : base(configuration, "Users")
        {
            _restProvider = restProvider;
        }

        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            var response = await _restProvider.GetAsync(Endpoint, null);
            var items = JsonProvider.DeserializeSimple<IEnumerable<UserDTO>>(response);
            return items ?? new List<UserDTO>();
        }

        public async Task<UserDTO?> GetUserAsync(int id)
        {
            var response = await _restProvider.GetAsync(Endpoint, id.ToString());
            return JsonProvider.DeserializeSimple<UserDTO>(response);
        }

        public async Task<bool> CreateUserAsync(UserDTO dto)
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

        public async Task<bool> UpdateUserAsync(int id, UserDTO dto)
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

        public async Task<bool> DeleteUserAsync(int id)
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
