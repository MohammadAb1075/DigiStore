using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DigiStore.Controllers
{
    public class BaseController:Controller
    {
        public BaseController():base()
        {

        }
        public IActionResult Index() => View();
    }
}
