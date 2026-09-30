using APW.Architecture;
using PAW.Architecture.Providers;
using PAW.Models.DTO;

namespace TareaPAW.Services
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryDTO>> GetInventoriesAsync();
        Task<InventoryDTO?> GetInventoryAsync(int id);
        Task<bool> CreateInventoryAsync(InventoryDTO dto);
        Task<bool> UpdateInventoryAsync(int id, InventoryDTO dto);
        Task<bool> DeleteInventoryAsync(int id);
    }

    public class InventoryService : ServiceBase, IInventoryService
    {
        private readonly IRestProvider _restProvider;

        public InventoryService(IRestProvider restProvider, IConfiguration configuration)
            : base(configuration, "Inventories")
        {
            _restProvider = restProvider;
        }

        public async Task<IEnumerable<InventoryDTO>> GetInventoriesAsync()
        {
            var response = await _restProvider.GetAsync(Endpoint, null);
            var items = JsonProvider.DeserializeSimple<IEnumerable<InventoryDTO>>(response);
            return items ?? new List<InventoryDTO>();
        }

        public async Task<InventoryDTO?> GetInventoryAsync(int id)
        {
            var response = await _restProvider.GetAsync(Endpoint, id.ToString());
            return JsonProvider.DeserializeSimple<InventoryDTO>(response);
        }

        public async Task<bool> CreateInventoryAsync(InventoryDTO dto)
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

        public async Task<bool> UpdateInventoryAsync(int id, InventoryDTO dto)
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

        public async Task<bool> DeleteInventoryAsync(int id)
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
