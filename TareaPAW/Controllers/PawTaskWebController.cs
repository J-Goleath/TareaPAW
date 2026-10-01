using Microsoft.AspNetCore.Mvc;
using TareaPAW.Services;

namespace TareaPAW.Controllers
{
    public class PawTaskWebController : Controller
    {
        private readonly IPawTaskService _service;

        public PawTaskWebController(IPawTaskService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _service.GetPawTasksAsync();

            return View(items);
        }
    }
}
