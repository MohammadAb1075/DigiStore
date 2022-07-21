using Microsoft.AspNetCore.Mvc;

namespace DigiStore.Controllers
{
    public class ChangeCultureController : Controller
	{
		[HttpGet]
		public Microsoft.AspNetCore.Mvc.IActionResult Index(string? cultureName)
		{
			var typedHeaders =
				HttpContext.Request.GetTypedHeaders();

			var httpReferer =
				typedHeaders?.Referer?.AbsoluteUri;

			if (string.IsNullOrWhiteSpace(httpReferer)) { 
			
				return RedirectToAction(actionName: "Index",controllerName: "Home");
			}


			string defaultCultureName = "fa";

            if (string.IsNullOrEmpty(cultureName)) 
			{
				cultureName = defaultCultureName; 
			}

			cultureName = cultureName
					.Replace(" ", string.Empty)
					.ToLower()
					.Substring(0, 2);
            
			switch (cultureName)
            {
				case "fa":
				case "en":
                {
                    break;
                }

                default:
                {
					cultureName = defaultCultureName;
					break;
                }
            }

			Infrastructure.Middlewares
				.CultureCookieHandlerMiddleware
				.SetCulture(cultureName: cultureName);

			Infrastructure.Middlewares
				.CultureCookieHandlerMiddleware
				.CreateCookie(httpContext: HttpContext, cultureName: cultureName!);

			return Redirect(url: httpReferer);
		}
	}
}


//using Microsoft.AspNetCore.Mvc;

//using System.Linq;
//using Microsoft.AspNetCore.Http;

//namespace DigiStore.Controllers
//{
//    public class ChangeCultureController : Controller
//    {

//		public ChangeCultureController
//			(Microsoft.Extensions.Options.IOptions
//			<Infrastructure.Settings.ApplicationSettings> applicationSettingsOptions) : base()
//		{
//			ApplicationSettings =
//				applicationSettingsOptions.Value;
//		}

//		private Infrastructure.Settings.ApplicationSettings ApplicationSettings { get; }

//        [HttpGet]
//		public Microsoft.AspNetCore.Mvc.IActionResult Index(string? cultureName)
//			{
//				// **************************************************
//				// GetTypedHeaders -> using Microsoft.AspNetCore.Http;
//				var typedHeaders =
//					HttpContext.Request.GetTypedHeaders();

//				var httpReferer =
//					typedHeaders?.Referer?.AbsoluteUri;

//				if (string.IsNullOrWhiteSpace(httpReferer))
//				{
//					return RedirectToAction(actionName: "Index", controllerName:"Home");
//				}
//				// **************************************************

//				// **************************************************
//				var defaultCultureName =
//					ApplicationSettings.CultureSettings?.DefaultCultureName;

//				var supportedCultureNames =
//					ApplicationSettings.CultureSettings?.SupportedCultureNames?
//					.ToList()
//					;
//				// **************************************************

//				// **************************************************
//				if (string.IsNullOrWhiteSpace(cultureName))
//				{
//					cultureName =
//						defaultCultureName;
//				}
//				// **************************************************

//				// **************************************************
//				if (supportedCultureNames == null ||
//					supportedCultureNames.Contains(item: cultureName!) == false)
//				{
//					cultureName =
//						defaultCultureName;
//				}
//				// **************************************************

//				// **************************************************
//				Infrastructure.Middlewares
//					.CultureCookieHandlerMiddleware.SetCulture(cultureName: cultureName);
//				// **************************************************

//				// **************************************************
//				Infrastructure.Middlewares.CultureCookieHandlerMiddleware
//					.CreateCookie(httpContext: HttpContext, cultureName: cultureName!);
//				// **************************************************

//				return Redirect(url: httpReferer);
//			}
//	}
//}