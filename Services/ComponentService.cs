using APW.Architecture;
using PAW.Architecture.Providers;
using PAW.Models.DTO;

namespace TareaPAW.Services
{
    public interface IComponentService
    {
        Task<IEnumerable<ComponentDTO>> GetComponentsAsync();
        Task<ComponentDTO?> GetComponentAsync(int id);
        Task<bool> CreateComponentAsync(ComponentDTO dto);
        Task<bool> UpdateComponentAsync(int id, ComponentDTO dto);
        Task<bool> DeleteComponentAsync(int id);
    }

    public class ComponentService : ServiceBase, IComponentService
    {
        private readonly IRestProvider _restProvider;

        public ComponentService(IRestProvider restProvider, IConfiguration configuration)
            : base(configuration, "Components")
        {
            _restProvider = restProvider;
        }

        public async Task<IEnumerable<ComponentDTO>> GetComponentsAsync()
        {
            var response = await _restProvider.GetAsync(Endpoint, null);
            var items = JsonProvider.DeserializeSimple<IEnumerable<ComponentDTO>>(response);
            return items ?? new List<ComponentDTO>();
        }

        public async Task<ComponentDTO?> GetComponentAsync(int id)
        {
            var response = await _restProvider.GetAsync(Endpoint, id.ToString());
            return JsonProvider.DeserializeSimple<ComponentDTO>(response);
        }

        public async Task<bool> CreateComponentAsync(ComponentDTO dto)
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

        public async Task<bool> UpdateComponentAsync(int id, ComponentDTO dto)
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

        public async Task<bool> DeleteComponentAsync(int id)
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
