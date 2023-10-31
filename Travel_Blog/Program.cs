using Microsoft.EntityFrameworkCore;
using Travel_Blog.Context;
using Microsoft.AspNetCore.Identity;
using Travel_Blog.Models;
using Travel_Blog.External;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAuthentication()
.AddCookie()
.AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration["CredentialsforGoogle:ClientId"];
    googleOptions.ClientSecret = builder.Configuration["CredentialsforGoogle:ClientSecret"];
});
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<DBContext>();
builder.Services.AddScoped<BlobService>();

builder.Services.AddSignalR().AddAzureSignalR(builder.Configuration["SignalRConn"]);
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.UseAzureSignalR(conn =>
{
    conn.MapHub<SignalRService>("/signalrservice");
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Blog}/{action=Index}/{id?}");

app.Run();
