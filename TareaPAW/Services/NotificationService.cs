using APW.Architecture;
using PAW.Architecture.Providers;
using PAW.Models.DTO;

namespace TareaPAW.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationDTO>> GetNotificationsAsync();
        Task<NotificationDTO?> GetNotificationAsync(int id);
        Task<bool> CreateNotificationAsync(NotificationDTO dto);
        Task<bool> UpdateNotificationAsync(int id, NotificationDTO dto);
        Task<bool> DeleteNotificationAsync(int id);
    }

    public class NotificationService : ServiceBase, INotificationService
    {
        private readonly IRestProvider _restProvider;

        public NotificationService(IRestProvider restProvider, IConfiguration configuration)
            : base(configuration, "Notifications")
        {
            _restProvider = restProvider;
        }

        public async Task<IEnumerable<NotificationDTO>> GetNotificationsAsync()
        {
            var response = await _restProvider.GetAsync(Endpoint, null);
            var items = JsonProvider.DeserializeSimple<IEnumerable<NotificationDTO>>(response);
            return items ?? new List<NotificationDTO>();
        }

        public async Task<NotificationDTO?> GetNotificationAsync(int id)
        {
            var response = await _restProvider.GetAsync(Endpoint, id.ToString());
            return JsonProvider.DeserializeSimple<NotificationDTO>(response);
        }

        public async Task<bool> CreateNotificationAsync(NotificationDTO dto)
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

        public async Task<bool> UpdateNotificationAsync(int id, NotificationDTO dto)
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

        public async Task<bool> DeleteNotificationAsync(int id)
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
