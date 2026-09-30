using Microsoft.AspNetCore.Mvc;
using TareaPAW.Services;

namespace TareaPAW.Controllers
{
    public class UserRoleWebController : Controller
    {
        private readonly IUserRoleService _service;

        public UserRoleWebController(IUserRoleService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _service.GetUserRolesAsync();

            return View(items);
        }
    }
}
