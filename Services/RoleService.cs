using APW.Architecture;
using PAW.Architecture.Providers;
using PAW.Models.DTO;

namespace TareaPAW.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDTO>> GetRolesAsync();
        Task<RoleDTO?> GetRoleAsync(int id);
        Task<bool> CreateRoleAsync(RoleDTO dto);
        Task<bool> UpdateRoleAsync(int id, RoleDTO dto);
        Task<bool> DeleteRoleAsync(int id);
    }

    public class RoleService : ServiceBase, IRoleService
    {
        private readonly IRestProvider _restProvider;

        public RoleService(IRestProvider restProvider, IConfiguration configuration)
            : base(configuration, "Roles")
        {
            _restProvider = restProvider;
        }

        public async Task<IEnumerable<RoleDTO>> GetRolesAsync()
        {
            var response = await _restProvider.GetAsync(Endpoint, null);
            var items = JsonProvider.DeserializeSimple<IEnumerable<RoleDTO>>(response);
            return items ?? new List<RoleDTO>();
        }

        public async Task<RoleDTO?> GetRoleAsync(int id)
        {
            var response = await _restProvider.GetAsync(Endpoint, id.ToString());
            return JsonProvider.DeserializeSimple<RoleDTO>(response);
        }

        public async Task<bool> CreateRoleAsync(RoleDTO dto)
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

        public async Task<bool> UpdateRoleAsync(int id, RoleDTO dto)
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

        public async Task<bool> DeleteRoleAsync(int id)
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
