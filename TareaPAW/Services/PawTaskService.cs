using APW.Architecture;
using PAW.Architecture.Providers;
using PAW.Models.DTO;

namespace TareaPAW.Services
{
    public interface IPawTaskService
    {
        Task<IEnumerable<PawTaskDTO>> GetPawTasksAsync();
        Task<PawTaskDTO?> GetPawTaskAsync(int id);
        Task<bool> CreatePawTaskAsync(PawTaskDTO dto);
        Task<bool> UpdatePawTaskAsync(int id, PawTaskDTO dto);
        Task<bool> DeletePawTaskAsync(int id);
    }

    public class PawTaskService : ServiceBase, IPawTaskService
    {
        private readonly IRestProvider _restProvider;

        public PawTaskService(IRestProvider restProvider, IConfiguration configuration)
            : base(configuration, "PawTasks")
        {
            _restProvider = restProvider;
        }

        public async Task<IEnumerable<PawTaskDTO>> GetPawTasksAsync()
        {
            var response = await _restProvider.GetAsync(Endpoint, null);
            var items = JsonProvider.DeserializeSimple<IEnumerable<PawTaskDTO>>(response);
            return items ?? new List<PawTaskDTO>();
        }

        public async Task<PawTaskDTO?> GetPawTaskAsync(int id)
        {
            var response = await _restProvider.GetAsync(Endpoint, id.ToString());
            return JsonProvider.DeserializeSimple<PawTaskDTO>(response);
        }

        public async Task<bool> CreatePawTaskAsync(PawTaskDTO dto)
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

        public async Task<bool> UpdatePawTaskAsync(int id, PawTaskDTO dto)
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

        public async Task<bool> DeletePawTaskAsync(int id)
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
