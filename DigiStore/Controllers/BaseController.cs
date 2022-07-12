using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DigiStore.Controllers
{
    public class BaseController:Controller
    {
        public BaseController():base()
        {

        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //var culture = Convert.ToString(ControllerContext.RouteData.Values["culture"]);
            var culture = ControllerContext.RouteData.Values["culture"]?.ToString();
            culture = String.IsNullOrEmpty(culture) ? "fa" : culture;
            ViewData["lang"] = culture;
            //ViewBag.lang = culture;
            ViewData["RTL"] = culture.ToLower() == "fa" ? true: false;
            base.OnActionExecuting(context);
        }
    }
}
