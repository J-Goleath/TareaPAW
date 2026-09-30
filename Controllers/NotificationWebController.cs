using Microsoft.AspNetCore.Mvc;
using TareaPAW.Services;

namespace TareaPAW.Controllers
{
    public class NotificationWebController : Controller
    {
        private readonly INotificationService _service;

        public NotificationWebController(INotificationService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _service.GetNotificationsAsync();

            return View(items);
        }
    }
}
