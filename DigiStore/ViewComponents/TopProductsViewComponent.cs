using DigiStore.Entities;
using DigiStore.Helper;
using DigiStore.Models;
using DigiStore.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DigiStore.ViewComponents
{
    public class TopProductsViewComponent : ViewComponent
    {
        private IProductHelper _productHelper;
        private IGenericRepository<Products> _genericRepository;

        public TopProductsViewComponent(IProductHelper productHelper, IGenericRepository<Products> genericRepository)
        {
            _productHelper = productHelper;
            _genericRepository = genericRepository;
        }

        #region SampleDB
        //public async Task<IViewComponentResult> InvokeAsync(int count)
        //{
        //    var topProducts = SampleDB.Products.OrderByDescending(q => q.Price).Take(count).ToList();
        //    return View(topProducts);
        //}
        #endregion

        #region Dapper
        //public async Task<IViewComponentResult> InvokeAsync(int count)
        //{
        //    //var topProducts = _productHelper.GetAllAsync().Result;//.OrderByDescending(q => q.Price).Take(count).ToList();
        //    //var topProducts = _productHelper.GetAllWithSPAsync(count);//.OrderByDescending(q => q.Price).Take(count).ToList();
        //    var topProducts = _productHelper.GetAllWithSPAsync(count).Result;
        //    return View(topProducts);
        //}
        #endregion


        #region EFCore
        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            var topProducts = _genericRepository.GetAllAsync(count).Result;
            return View(topProducts);
        }
        #endregion


    }
}