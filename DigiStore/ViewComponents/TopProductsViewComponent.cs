using DigiStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigiStore.ViewComponents
{
    public class TopProductsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            var topProducts = SampleDB.Products.OrderByDescending(q => q.Price).Take(count).ToList();
            return View(topProducts);
        }
    }
}