using DigiStore.Data;
using DigiStore.Entities;
using DigiStore.Helper;
using DigiStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigiStore.Controllers
{
    public class ProductController : BaseController
    {
        #region field
        private ILogger<ProductController> _logger;
        private DigiStoreContext _dbContext;
        private IProductHelper _productHelper;
        private ICategoryHelper _categoryHelper;

        #endregion

        #region Constructor
        //public ProductController(IProductHelper productHelper, ICategoryHelper categoryHelper)
        //{
        //    _productHelper = productHelper;
        //    _categoryHelper = categoryHelper;
        //}

        //public ProductController(ILogger<ProductController> logger, DigiStoreContext dbContext)
        //{
        //    this.logger = logger;
        //    _dbContext = dbContext;
        //}
        public ProductController(ILogger<ProductController> logger, DigiStoreContext dbContext, IProductHelper productHelper, ICategoryHelper categoryHelper)
        {
            _logger = logger;
            _dbContext = dbContext;
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

        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    ViewBag.Title = "ViewBag Product List";
        //    ViewData["Title"] = "ViewData Product List";

        //    //var model = await _productHelper.GetAllAsync();
        //    var model = await _productHelper.GetAllWithSPAsync();
        //    var (products, categories) = await _productHelper.GetMultipleProducts_CategoriesAsync();
        //    return View(products);
        //}

        #endregion
        
        #region EFCore

        [HttpGet]
        public async Task<IActionResult> Index(int page)
        {
            _logger.LogInformation($"Show Product List With Browser : {Request.Headers["User_Agent"]}");
            //_logger.LogInformation($"Show Product List With Browser : {Request.Headers.UserAgent}");
            ViewData["Title"] = "ViewData Product List";
            var result = _dbContext.Products.Select(q => new Products
            {
                ProductId = q.ProductId,
                CategoryId = q.CategoryId,
                ProductName = q.ProductName,
                Price = q.Price
            });
            return View(result.ToList());
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
        //private async Task GetCategories()
        //{
        //    var categories = await _categoryHelper.GetAllAsync();
        //    ViewData["Categories"] = categories;
        //}

        //[HttpGet]
        //public async Task<IActionResult> Create()
        //{
        //    await GetCategories();
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(ProductModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _productHelper.AddWithSPAsync(model);
        //        return RedirectToAction("Index");
        //    }
        //    else {
        //        await GetCategories();
        //        return View(model); 
        //    }
        //}

        #endregion

        #region EFCore
        private async Task GetCategories()
        {
            var categories = _dbContext.Categories.Select(x => new CategoryModel
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName
            }).ToList();
            ViewData["Categories"] = categories;
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
                var productModel = new Products
                {
                    CategoryId = model.CategoryId,
                    ProductName = model.ProductName,
                    Price = model.Price,                    
                };
                await _dbContext.AddAsync(productModel);
                //await _dbContext.Products.AddAsync(productModel);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
                //return RedirectToAction("Create");
                //return view(model);
            }
            else {
                await GetCategories();
                // ModelState.AddModelError("",Resource) => Resources
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

        //[HttpGet]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    await GetCategories();
        //    var model = await _productHelper.GetByIdWithSPAsync(id);
        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(int id, ProductModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //await _productHelper.UpdateWithSPAsync(model);
        //        //var entity = await _productHelper.GetByIdWithSPAsync(model.ProductId);
        //        var entity = await _productHelper.GetByIdWithSPAsync(id);
        //        entity.CategoryId = model.CategoryId;
        //        entity.ProductName = model.ProductName;
        //        entity.Price = model.Price;
        //        await _productHelper.UpdateWithSPAsync(entity);
        //        return RedirectToAction("Index");
        //    }
        //    else {
        //        await GetCategories();
        //        return View(model); 
        //    }
        //}
        #endregion
        
        #region EFCore

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            await GetCategories();
            //var model = _dbContext.Find<Products>(id);
            var model = await _dbContext.Products.FindAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = await _dbContext.Products.FindAsync(id);
                entity.CategoryId = model.CategoryId;
                entity.ProductName = model.ProductName;
                entity.Price = model.Price;
                _dbContext.Products.Attach(entity);
                await _dbContext.SaveChangesAsync();
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
        //[HttpGet]
        //public IActionResult Delete(int id)
        //{
        //    _productHelper.DeleteWithSPAsync(id);
        //    return RedirectToAction("index");
        //}
        #endregion
        
        #region EFCore
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var item = _dbContext.Find<Products>(id);
            _dbContext.Remove(item);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("index");
        }
        #endregion

        #endregion

    }
}
