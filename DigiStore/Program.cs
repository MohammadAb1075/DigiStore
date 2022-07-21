using DigiStore.Data;
using DigiStore.Entities;
using DigiStore.Helper;
using DigiStore.Repositories;
using DigiStore.Utilities;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DigiStoreContext>();
builder.Services.AddScoped<ISQLUtitily, SQLUtitily>();
builder.Services.AddScoped<IProductHelper, ProductHelper>();
builder.Services.AddScoped<ICategoryHelper, CategoryHelper>();
builder.Services.AddScoped<IGenericRepository<Products>, GenericRepository<Products>>();



#region Set Cultures

var supportedCultures = new[]
{
    new CultureInfo("en"),
    new CultureInfo("fa")
};

var opts = new RequestLocalizationOptions()
{
    DefaultRequestCulture = new RequestCulture(culture: "fa", uiCulture: "fa"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};

opts.RequestCultureProviders = new[]
{
    new RouteDataRequestCultureProvider(){ Options = opts}
};

builder.Services.AddSingleton(opts);

//builder.Services.Configure<RequestLocalizationOptions>(options =>
//{
//    var supportedCultures = new[]
//    {
//        new System.Globalization.CultureInfo(name: "fa-IR"),
//        new System.Globalization.CultureInfo(name: "en-US"),
//		//new System.Globalization.CultureInfo(name: "fr-FR"),
//	};

//    options.SupportedCultures = supportedCultures;
//    options.SupportedUICultures = supportedCultures;

//    options.DefaultRequestCulture =
//        new Microsoft.AspNetCore.Localization
//        .RequestCulture(culture: "fa-IR", uiCulture: "fa-IR");
//});




#endregion



builder.Services.AddControllersWithViews();
var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}




app.UseDeveloperExceptionPage();
app.UseStatusCodePages();
app.UseStaticFiles();
//app.UseSession();
app.UseRouting();
app.UseRequestLocalization(opts);

// UseCultureCookie() -> using Infrastructure.Middlewares;
//app.UseCultureCookie();
app.UseMiddleware<Infrastructure.Middlewares.CultureCookieHandlerMiddleware>();

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("default", "/{controller=Home}/{action=index}/{id?}");
    endpoints.MapControllerRoute("defaultCulture", "/{culture?}/{controller=Home}/{action=index}/{id?}", constraints: new { culture = "fa|en" });
    //endpoints.MapControllerRoute("pagination", "/{controller=Category}/{action=index}/{category}/Page-{PageNumber}");
    //endpoints.MapControllerRoute("pagination", "/{controller=Category}/{action=index}/Page-{PageNumber}");
    //endpoints.MapControllerRoute("pagination", "/{controller=Category}/{action=index}/{category}");
    endpoints.MapDefaultControllerRoute();
});

app.Run();
