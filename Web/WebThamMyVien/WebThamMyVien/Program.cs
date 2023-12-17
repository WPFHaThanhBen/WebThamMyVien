using Microsoft.Extensions.Configuration;
using WebThamMyVien.API;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;
using WebThamMyVien.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<ConnectAPI>(builder.Configuration.GetSection("ConnectionStrings"));


builder.Services.AddHttpClient("ConnectAPI", client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetSection("ConnectionStrings")["StringConnectAPI"]);
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    // Thêm định tuyến controller mặc định cho không có khu vực (global)
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});
app.Run();
