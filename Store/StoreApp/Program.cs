
using StoreApp.Infrastructe.Extensions;
using StoreApp.Infrastructe.Mapper;
using StoreApp.Models;

var builder = WebApplication.CreateBuilder(args);




builder.Services.ConfigureSession();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.ConfigureRepositoryRegistration();
builder.Services.ConfigureServiceRegistration();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureRouting();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureIdentity();

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    
    endpoints.MapAreaControllerRoute("AdminRoute","Admin","admin/{controller=Dashboard}/{action=Index}/{id?}");
    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
 
});


app.UseConfigureAndCheckMigration();
app.UseConfigureLocalization();
app.ConfigureDefaultAdminUser();
app.Run();
