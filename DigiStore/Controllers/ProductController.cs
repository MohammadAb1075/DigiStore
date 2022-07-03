using DigiStore.Helper;
using DigiStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigiStore.Controllers
{
    public class ProductController : Controller
    {
        #region field

        private IProductHelper _productHelper;
        private ICategoryHelper _categoryHelper;

        #endregion

        #region Constructor
        public ProductController(IProductHelper productHelper, ICategoryHelper categoryHelper)
        {
            _productHelper = productHelper;
            _categoryHelper = categoryHelper;
        }
        #endregion

        #region Get

        #region SampleDB

        //[HttpGet]
        //public IActionResult Index()
        //{
        //    ViewData["Title"] = "Product List";
        //    var products = SampleDB.Products;
        //    return View(products);
        //}
        //[HttpGet]
        //public IActionResult List()
        //{
        //    return Json(SampleDB.Products);
        //}

        #endregion

        #region Dapper

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "ViewBag Product List";
            ViewData["Title"] = "ViewData Product List";

            //var model = await _productHelper.GetAllAsync();
            var model = await _productHelper.GetAllWithSPAsync();
            var (products, categories) = await _productHelper.GetMultipleProducts_CategoriesAsync();
            return View(products);
        }

        #endregion

        #endregion

        #region Create

        #region SampleDB

        //[HttpGet]
        //public IActionResult Create()
        //{
        //    ViewData["Categories"] = SampleDB.Categories;
        //    //@ViewData["Title"] = "Create";
        //    var products = SampleDB.Products;
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Create(ProductModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        SampleDB.Products.Add(model);
        //        return RedirectToAction("Index");
        //        //return View(model);
        //    }
        //    else { return View(model); }
        //}

        #endregion

        #region Dapper
        private async Task GetCategories()
        {
            var categories = await _categoryHelper.GetAllAsync();
            ViewData["Categories"] = await _categoryHelper.GetAllAsync();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await GetCategories();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _productHelper.AddWithSPAsync(model);
                return RedirectToAction("Index");
            }
            else {
                await GetCategories();
                return View(model); 
            }
        }

        #endregion

        #endregion

        #region Edit

        #region SampleDB

        //[HttpGet]
        //public IActionResult Edit(int id)
        //{
        //    ViewData["Categories"] = SampleDB.Categories;
        //    //@ViewData["Title"] = "Edit";
        //    //var product = SampleDB.Products.FirstOrDefault(x => x.ProductId.Equals(id));
        //    var product = SampleDB.Products.Find(x => x.ProductId.Equals(id));
        //    return View(product);
        //}

        //[HttpPost]
        //public IActionResult Edit(ProductModel model)
        //{
        //    //if (model == null || !ModelState.IsValid) { 
        //    //    return View(model);
        //    //}
        //    //else
        //    //{
        //    var product = SampleDB.Products.Find(x => x.ProductId.Equals(model.ProductId));
        //    if (product != null)
        //    {
        //        product.ProductName = model.ProductName;
        //        product.Price = model.Price;
        //    }
        //    return RedirectToAction("Index");
        //    // }

        //    //int index = SampleDB.Products.FindIndex(q => q.ProductId.Equals(id));

        //    //if (index > -1)
        //    //{
        //    //    model.ProductId = id;
        //    //    SampleDB.Products[index] = model;
        //    //    return RedirectToAction("Index");
        //    //}
        //    //return View(model);

        //}

        #endregion

        #region Dapper

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            await GetCategories();
            var model = await _productHelper.GetByIdWithSPAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductModel model)
        {
            if (ModelState.IsValid)
            {
                //await _productHelper.UpdateWithSPAsync(model);
                //var entity = await _productHelper.GetByIdWithSPAsync(model.ProductId);
                var entity = await _productHelper.GetByIdWithSPAsync(id);
                entity.CategoryId = model.CategoryId;
                entity.ProductName = model.ProductName;
                entity.Price = model.Price;
                await _productHelper.UpdateWithSPAsync(entity);
                return RedirectToAction("Index");
            }
            else {
                await GetCategories();
                return View(model); 
            }
        }
        #endregion

        #endregion

        #region Delete

        #region SampleDB
        //[HttpGet]
        //public IActionResult Delete(int id)
        //{
        //    var index = SampleDB.Products.FindIndex(x => x.ProductId.Equals(id));
        //    if (index != -1)
        //    {
        //        SampleDB.Products.RemoveAt(index);
        //    }
        //    return RedirectToAction("index");
        //}
        #endregion

        #region Dapper
        [HttpGet]
        public IActionResult Delete(int id)
        {
            _productHelper.DeleteWithSPAsync(id);
            return RedirectToAction("index");
        }
        #endregion

        #endregion

    }
}
