using APW.Architecture;
using PAW.Architecture.Providers;
using PAW.Models.DTO;

namespace TareaPAW.Services
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierDTO>> GetSuppliersAsync();
        Task<SupplierDTO?> GetSupplierAsync(int id);
        Task<bool> CreateSupplierAsync(SupplierDTO dto);
        Task<bool> UpdateSupplierAsync(int id, SupplierDTO dto);
        Task<bool> DeleteSupplierAsync(int id);
    }

    public class SupplierService : ServiceBase, ISupplierService
    {
        private readonly IRestProvider _restProvider;

        public SupplierService(IRestProvider restProvider, IConfiguration configuration)
            : base(configuration, "Suppliers")
        {
            _restProvider = restProvider;
        }

        public async Task<IEnumerable<SupplierDTO>> GetSuppliersAsync()
        {
            var response = await _restProvider.GetAsync(Endpoint, null);
            var items = JsonProvider.DeserializeSimple<IEnumerable<SupplierDTO>>(response);
            return items ?? new List<SupplierDTO>();
        }

        public async Task<SupplierDTO?> GetSupplierAsync(int id)
        {
            var response = await _restProvider.GetAsync(Endpoint, id.ToString());
            return JsonProvider.DeserializeSimple<SupplierDTO>(response);
        }

        public async Task<bool> CreateSupplierAsync(SupplierDTO dto)
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

        public async Task<bool> UpdateSupplierAsync(int id, SupplierDTO dto)
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

        public async Task<bool> DeleteSupplierAsync(int id)
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
