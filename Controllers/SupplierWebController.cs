using Microsoft.AspNetCore.Mvc;
using TareaPAW.Services;

namespace TareaPAW.Controllers
{
    public class SupplierWebController : Controller
    {
        private readonly ISupplierService _service;

        public SupplierWebController(ISupplierService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _service.GetSuppliersAsync();

            return View(items);
        }
    }
}
