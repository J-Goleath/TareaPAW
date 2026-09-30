using Microsoft.AspNetCore.Mvc;
using TareaPAW.Services;

namespace TareaPAW.Controllers
{
    public class RoleWebController : Controller
    {
        private readonly IRoleService _service;

        public RoleWebController(IRoleService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _service.GetRolesAsync();

            return View(items);
        }
    }
}
