using Microsoft.AspNetCore.Mvc;
using TareaPAW.Services;

namespace TareaPAW.Controllers
{
    public class ComponentWebController : Controller
    {
        private readonly IComponentService _service;

        public ComponentWebController(IComponentService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _service.GetComponentsAsync();

            return View(items);
        }
    }
}
