using APW.Architecture;
using PAW.Architecture.Providers;
using PAW.Models.DTO;

namespace TareaPAW.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProductsAsync();
        Task<ProductDTO?> GetProductAsync(int id);
        Task<bool> CreateProductAsync(ProductDTO dto);
        Task<bool> UpdateProductAsync(int id, ProductDTO dto);
        Task<bool> DeleteProductAsync(int id);
    }

    public class ProductService : ServiceBase, IProductService
    {
        private readonly IRestProvider _restProvider;

        public ProductService(IRestProvider restProvider, IConfiguration configuration)
            : base(configuration, "Products")
        {
            _restProvider = restProvider;
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            var response = await _restProvider.GetAsync(Endpoint, null);
            var items = JsonProvider.DeserializeSimple<IEnumerable<ProductDTO>>(response);
            return items ?? new List<ProductDTO>();
        }

        public async Task<ProductDTO?> GetProductAsync(int id)
        {
            var response = await _restProvider.GetAsync(Endpoint, id.ToString());
            return JsonProvider.DeserializeSimple<ProductDTO>(response);
        }

        public async Task<bool> CreateProductAsync(ProductDTO dto)
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

        public async Task<bool> UpdateProductAsync(int id, ProductDTO dto)
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

        public async Task<bool> DeleteProductAsync(int id)
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
