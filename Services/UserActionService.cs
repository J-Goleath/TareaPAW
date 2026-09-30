using APW.Architecture;
using PAW.Architecture.Providers;
using PAW.Models.DTO;

namespace TareaPAW.Services
{
    public interface IUserActionService
    {
        Task<IEnumerable<UserActionDTO>> GetUserActionsAsync();
        Task<UserActionDTO?> GetUserActionAsync(int id);
        Task<bool> CreateUserActionAsync(UserActionDTO dto);
        Task<bool> UpdateUserActionAsync(int id, UserActionDTO dto);
        Task<bool> DeleteUserActionAsync(int id);
    }

    public class UserActionService : ServiceBase, IUserActionService
    {
        private readonly IRestProvider _restProvider;

        public UserActionService(IRestProvider restProvider, IConfiguration configuration)
            : base(configuration, "UserActions")
        {
            _restProvider = restProvider;
        }

        public async Task<IEnumerable<UserActionDTO>> GetUserActionsAsync()
        {
            var response = await _restProvider.GetAsync(Endpoint, null);
            var items = JsonProvider.DeserializeSimple<IEnumerable<UserActionDTO>>(response);
            return items ?? new List<UserActionDTO>();
        }

        public async Task<UserActionDTO?> GetUserActionAsync(int id)
        {
            var response = await _restProvider.GetAsync(Endpoint, id.ToString());
            return JsonProvider.DeserializeSimple<UserActionDTO>(response);
        }

        public async Task<bool> CreateUserActionAsync(UserActionDTO dto)
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

        public async Task<bool> UpdateUserActionAsync(int id, UserActionDTO dto)
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

        public async Task<bool> DeleteUserActionAsync(int id)
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
