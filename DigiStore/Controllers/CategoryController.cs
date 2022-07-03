using DigiStore.Helper;
using Microsoft.AspNetCore.Mvc;

namespace DigiStore.Controllers
{
    public class CategoryController : Controller
    {
        ICategoryHelper _categoryHelper;

        public CategoryController(ICategoryHelper categoryHelper)
        {
            _categoryHelper = categoryHelper;
        }

        public IActionResult Index(int page = 1)
        {
            var result = _categoryHelper.GetWithPaginationAsync(page);
            return View(result);
        }
    }
}
