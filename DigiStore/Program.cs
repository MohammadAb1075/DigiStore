using DigiStore.Data;
using DigiStore.Helper;
using DigiStore.Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DigiStoreContext>();
builder.Services.AddScoped<ISQLUtitily, SQLUtitily>();
builder.Services.AddScoped<IProductHelper, ProductHelper>();
builder.Services.AddScoped<ICategoryHelper, CategoryHelper>();
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
    //endpoints.MapControllerRoute("pagination", "/{controller=Category}/{action=index}/{category}/Page-{PageNumber}");
    //endpoints.MapControllerRoute("pagination", "/{controller=Category}/{action=index}/Page-{PageNumber}");
    //endpoints.MapControllerRoute("pagination", "/{controller=Category}/{action=index}/{category}");
    endpoints.MapDefaultControllerRoute();
});

app.Run();
