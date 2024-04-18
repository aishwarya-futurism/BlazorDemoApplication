//using BlazorDemoApplication.Areas.Identity;
//using BlazorDemoApplication.Data;
//using Blazored.SessionStorage;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.Components.Authorization;
//using Microsoft.AspNetCore.Components.Web;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.DependencyInjection;
//using System.Threading.Tasks;


//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//   .AddEntityFrameworkStores<ApplicationDbContext>();
//builder.Services.AddRazorPages();
//builder.Services.AddServerSideBlazor();
//builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
//builder.Services.AddSingleton<WeatherForecastService>();
//builder.Services.AddScoped<UserService>();
////builder.Services.AddScoped<UserRegister>();
//builder.Services.AddBlazoredSessionStorage();


////builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
////    .AddCookie(options =>
////    {
////        options.Cookie.Name = "auth_token";
////        options.LoginPath = "/login";
////        options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
////        options.AccessDeniedPath = "/access-denied";
////    }
//////);
//builder.Services.AddAuthorization();
////builder.Services.AddCascadingAuthenticationState();

//// Connection to the Database

////builder.Services.AddDbContext<ApplicationDbContext>(item => item.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseMigrationsEndPoint();
//}
//else
//{
//    app.UseExceptionHandler("/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();

//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllers();
//app.MapBlazorHub();
//app.MapFallbackToPage("/_Host");

//app.Run();















//using BlazorDemoApplication.Areas.Identity;
//using BlazorDemoApplication.Data;
//using Blazored.SessionStorage;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Components.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Syncfusion.Blazor;


//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//   .AddEntityFrameworkStores<ApplicationDbContext>();
//builder.Services.AddRazorPages();
//builder.Services.AddServerSideBlazor();
//builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
//builder.Services.AddSingleton<WeatherForecastService>();
////builder.Services.AddSingleton<UploadFileInfo>();
//builder.Services.AddScoped<UserService>();
//builder.Services.AddBlazoredSessionStorage();
//builder.Services.AddScoped<User>();
//builder.Services.AddScoped<UserRegister>();
////builder.Services.AddScoped<Country>();
////builder.Services.AddScoped<State>();
////builder.Services.AddScoped<City>();
////builder.Services.AddScoped<UploadFileInfo>();

////upload image services
//builder.Services.AddDbContextFactory<ApplicationDbContext>(option => option.UseSqlServer("DefaultConnection"));

//builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer("DefaultConnection"));



//builder.Services.AddAuthorization();

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.Cookie.Name = "auth_token";
//        options.LoginPath = "/login";
//        options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
//        options.AccessDeniedPath = "/access-denied";
//    }
//);

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseMigrationsEndPoint();
//}
//else
//{
//    app.UseExceptionHandler("/Error");
//    app.UseHsts();
//}

//app.UseHttpsRedirection();

//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();
//app.UseAuthentication(); // Authentication should come after Authorization

//app.MapControllers();
//app.MapBlazorHub();
//app.MapFallbackToPage("/_Host");

//app.Run();




using BlazorDemoApplication.Areas.Identity;
using BlazorDemoApplication.Data;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<User>();
builder.Services.AddScoped<UserRegister>();
//builder.Services.AddScoped<Country>();
//builder.Services.AddScoped<State>();
//builder.Services.AddScoped<City>();
//builder.Services.AddScoped<UploadFileInfo>();

// Add IDbContextFactory<ApplicationDbContext> registration

//builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "auth_token";
        options.LoginPath = "/login";
        options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
        options.AccessDeniedPath = "/access-denied";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();












