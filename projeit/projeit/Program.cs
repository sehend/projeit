using Core.Models;
using Core.Repositories;
using Core.Services;
using Core.UnitOfWork;
using Data;
using Data.Repositories;
using Data.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using projeit.CustomValidatation;
using Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();




builder.Services.AddIdentity<AppUser, AppRole>(opts => { opts.Password.RequiredLength = 4; opts.Password.RequireNonAlphanumeric = false; opts.Password.RequireLowercase = false; opts.Password.RequireUppercase = false; opts.Password.RequireDigit = false; opts.User.RequireUniqueEmail = true; opts.User.AllowedUserNameCharacters = "abcçdefghýijklmnoöpqrsþtuüvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._"; }).AddPasswordValidator<Custompasswordvalidator>().AddUserValidator<CustomUserValidator>().AddErrorDescriber<CustomIdentityErrorsDescriber>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


builder.Services.AddDbContext<AppDbContext>(opt => { opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); });

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

builder.Services.AddScoped(typeof(IPersonService), typeof(PersonService));

builder.Services.AddScoped(typeof(IRoleService), typeof(RoleService));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddSession();


CookieBuilder cookieBuilder = new CookieBuilder();



cookieBuilder.Name = "MyBlog";

cookieBuilder.HttpOnly = false;





cookieBuilder.SameSite = SameSiteMode.Lax;

cookieBuilder.SecurePolicy = CookieSecurePolicy.SameAsRequest;

builder.Services.ConfigureApplicationCookie(opts =>

{

    opts.LoginPath = new PathString("/Home/Login");

    opts.LogoutPath = new PathString("/Member/LogOut");

    opts.Cookie = cookieBuilder;

    opts.SlidingExpiration = true;

    opts.ExpireTimeSpan = System.TimeSpan.FromDays(60);

    opts.AccessDeniedPath = new PathString("/Member/AccessDenied");

   

    opts.AccessDeniedPath = new PathString("/Member/AccessDenied");

});

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
app.UseStatusCodePages();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
