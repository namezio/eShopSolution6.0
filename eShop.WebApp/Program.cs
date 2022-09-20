
using eShop.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var services = builder.Services;
var app = builder.Build();
// services.AddDbContextPool<eShopEntities>(options => options.UseLazyLoadingProxies().UseMySQL(builder.Configuration.GetConnectionString("eShop")));
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// dotnet ef dbcontext scaffold "Host=127.0.0.1;Database=eShopDb;Username=root;Password=Kobiet99" MySql.EntityFrameworkCore --context eShopEntities --force
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();