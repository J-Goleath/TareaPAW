using Microsoft.AspNetCore.Mvc;
using TareaPAW.Services;

namespace TareaPAW.Controllers
{
    public class UserActionWebController : Controller
    {
        private readonly IUserActionService _service;

        public UserActionWebController(IUserActionService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _service.GetUserActionsAsync();

            return View(items);
        }
    }
}
