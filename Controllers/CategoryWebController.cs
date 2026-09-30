using Microsoft.AspNetCore.Mvc;
using TareaPAW.Services;

namespace TareaPAW.Controllers
{
    public class CategoryWebController : Controller
    {
        private readonly ICategoryService _service;

        public CategoryWebController(ICategoryService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _service.GetCategoriesAsync();

            return View(items);
        }
    }
}
