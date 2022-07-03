using DigiStore.Helper;
using DigiStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigiStore.ViewComponents
{
    public class TopProductsViewComponent : ViewComponent
    {
        private IProductHelper _productHelper;

        public TopProductsViewComponent(IProductHelper productHelper)
        {
            _productHelper = productHelper;
        }

        #region SampleDB
        //public async Task<IViewComponentResult> InvokeAsync(int count)
        //{
        //    var topProducts = SampleDB.Products.OrderByDescending(q => q.Price).Take(count).ToList();
        //    return View(topProducts);
        //}
        #endregion

        #region Dapper
        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            //var topProducts = _productHelper.GetAllAsync().Result;//.OrderByDescending(q => q.Price).Take(count).ToList();
            //var topProducts = _productHelper.GetAllWithSPAsync(count);//.OrderByDescending(q => q.Price).Take(count).ToList();
            var topProducts = _productHelper.GetAllWithSPAsync(count).Result;
            return View(topProducts);
        }
        #endregion
    }
}