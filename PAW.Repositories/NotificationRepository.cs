using PAW.Models;

namespace PAW.Repositories;

public interface INotificationRepository : IRepositoryBase<Notification>
{
}

public class NotificationRepository : RepositoryBase<Notification>, INotificationRepository
{
}
