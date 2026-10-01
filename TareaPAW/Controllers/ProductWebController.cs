using Microsoft.AspNetCore.Mvc;
using TareaPAW.Services;

namespace TareaPAW.Controllers
{
    public class ProductWebController : Controller
    {
        private readonly IProductService _service;

        public ProductWebController(IProductService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _service.GetProductsAsync();

            return View(items);
        }
    }
}
