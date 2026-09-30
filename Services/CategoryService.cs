using APW.Architecture;
using PAW.Architecture.Providers;
using PAW.Models.DTO;

namespace TareaPAW.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
        Task<CategoryDTO?> GetCategoryAsync(int id);
        Task<bool> CreateCategoryAsync(CategoryDTO dto);
        Task<bool> UpdateCategoryAsync(int id, CategoryDTO dto);
        Task<bool> DeleteCategoryAsync(int id);
    }

    public class CategoryService : ServiceBase, ICategoryService
    {
        private readonly IRestProvider _restProvider;

        public CategoryService(IRestProvider restProvider, IConfiguration configuration)
            : base(configuration, "Categories")
        {
            _restProvider = restProvider;
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync()
        {
            var response = await _restProvider.GetAsync(Endpoint, null);
            var items = JsonProvider.DeserializeSimple<IEnumerable<CategoryDTO>>(response);
            return items ?? new List<CategoryDTO>();
        }

        public async Task<CategoryDTO?> GetCategoryAsync(int id)
        {
            var response = await _restProvider.GetAsync(Endpoint, id.ToString());
            return JsonProvider.DeserializeSimple<CategoryDTO>(response);
        }

        public async Task<bool> CreateCategoryAsync(CategoryDTO dto)
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

        public async Task<bool> UpdateCategoryAsync(int id, CategoryDTO dto)
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

        public async Task<bool> DeleteCategoryAsync(int id)
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
