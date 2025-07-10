var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHttpContextAccessor()
    .AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection()
    .UseRouting()
    .UseStaticFiles()
    .UseAuthorization()
    .UseEndpoints( endpoint =>
    {
        endpoint.MapControllerRoute(
        name: "areaRoute",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/");

        endpoint.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/");
    });

await app.RunAsync();
