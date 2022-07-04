using DigiStore.Helper;
using DigiStore.Models;
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

        #region Get
        public IActionResult Index(int page = 1)
        {
            @ViewData["Title"] = "Category List";
            var result = _categoryHelper.GetWithPaginationAsync(page).Result;
            return View(result);
        }

        #endregion

        #region Create

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _categoryHelper.AddWithSPAsync(model);
            return RedirectToAction("Index");
        }

        #endregion

        #region Edit

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            @ViewData["Title"] = "Edit Category";
            var model = await _categoryHelper.GetByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var entity = await _categoryHelper.GetByIdAsync(id);
            entity.CategoryName = model.CategoryName;
            await _categoryHelper.UpdateWithSPAsync(id, entity);
            return RedirectToAction("Index");
        }

        #endregion

        #region Delete

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _categoryHelper.DeleteWithSPAsync(id);
            return RedirectToAction("index");
        }
        #endregion
    }
}
