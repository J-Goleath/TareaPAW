using Microsoft.AspNetCore.Mvc;
using TareaPAW.Services;

namespace TareaPAW.Controllers
{
    public class UserWebController : Controller
    {
        private readonly IUserService _service;

        public UserWebController(IUserService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _service.GetUsersAsync();

            return View(items);
        }
    }
}
