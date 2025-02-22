using Microsoft.EntityFrameworkCore;
using Myshop.DataAccess.Data;
using Myshop.DataAccess.Implementaion;
using Myshop.Entities.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Myshop.Utilities;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();
builder.Services.AddDbContext<ApplicationDbContext>(
        options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

builder.Services.AddIdentity<IdentityUser,IdentityRole>(
    options=> options.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromHours(2)
    ).AddDefaultTokenProviders().AddDefaultUI().
    AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Configuration.GetConnectionString("DefaultConnection")

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
app.MapRazorPages();

app.MapControllerRoute(
    name: "Customer",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Admin}/{controller=Home}/{action=Index}/{id?}");

app.Run();
