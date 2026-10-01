using Microsoft.AspNetCore.Mvc;
using TareaPAW.Services;

namespace TareaPAW.Controllers
{
    public class InventoryWebController : Controller
    {
        private readonly IInventoryService _service;

        public InventoryWebController(IInventoryService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _service.GetInventoriesAsync();

            return View(items);
        }
    }
}
