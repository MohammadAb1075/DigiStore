using DigiStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigiStore.Controllers
{
    public class ProductController:Controller
    {
        #region Get
        //SampleDB
        public IActionResult Index()
        {
            ViewData["Title"] = "Product List";
            var products = SampleDB.Products;
            return View(products);
        }
        public IActionResult List()
        {
            return Json(SampleDB.Products);
        }
        #endregion

        #region Create

        //SampleDB
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Categories"] = SampleDB.Categories;
            //@ViewData["Title"] = "Create";
            var products = SampleDB.Products;
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductModel model)
        {
            if(ModelState.IsValid)
            {
                SampleDB.Products.Add(model);
                return RedirectToAction("Index");
                //return View(model);
            }
            else { return View(model); }
        }
        #endregion

        #region Edit

        //SampleDB
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["Categories"] = SampleDB.Categories;
            //@ViewData["Title"] = "Edit";
            //var product = SampleDB.Products.FirstOrDefault(x => x.ProductId.Equals(id));
            var product = SampleDB.Products.Find(x => x.ProductId.Equals(id));
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(ProductModel model)
        {
            //if (model == null || !ModelState.IsValid) { 
            //    return View(model);
            //}
            //else
            //{
                var product = SampleDB.Products.Find(x => x.ProductId.Equals(model.ProductId));
                if (product != null)
                {
                    product.ProductName = model.ProductName;
                    product.Price = model.Price;
                }
                return RedirectToAction("Index");
           // }

            //int index = SampleDB.Products.FindIndex(q => q.ProductId.Equals(id));

            //if (index > -1)
            //{
            //    model.ProductId = id;
            //    SampleDB.Products[index] = model;
            //    return RedirectToAction("Index");
            //}
            //return View(model);

        }
        #endregion
        
        #region Delete

        //SampleDB
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var index = SampleDB.Products.FindIndex(x => x.ProductId.Equals(id));
            if (index != -1)
            {
                SampleDB.Products.RemoveAt(index);
            }
            return RedirectToAction("index");
        }
        #endregion

    }
}
