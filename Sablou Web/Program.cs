using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Sablou_Web.Models;
using Sablou_Web.Services;
using Sablou_Web.Services.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<cralle_dk_db_sablouContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddRazorPages(options =>
{
    // Angiv hvilke foldere login giver adgang til
    options.Conventions.AuthorizeFolder("/Ingredienser");
    options.Conventions.AuthorizeFolder("/Chokolader");
    options.Conventions.AuthorizePage("/OpdaterKatalog");
    options.Conventions.AuthorizePage("/RedigerKatalog");

});



builder.Services.AddSingleton<IDataService, Dataservice>();


builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
    {
        options.LoginPath = "/BrugerLogin/Login";
        options.AccessDeniedPath = "/BrugerLogin/AccessDenied";
        
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
