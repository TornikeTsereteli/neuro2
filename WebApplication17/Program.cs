using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using neurobalance.com.Models;
using Webinex.Chatify;
using Credential = neurobalance.com.Authorization.Credential;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();

builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
{
    options.Cookie.Name = "MyCookieAuth";
    // options.ExpireTimeSpan = TimeSpan.FromSeconds(200);
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("tokenOnly", policy =>
    {
        policy.RequireClaim("token");
    });
});

builder.Services.AddSingleton<LoginViewModel>();


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSingleton<LoginViewModel>();
// builder.Services.AddSingleton<Credential>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // app.UseExceptionHandler("/Home/Error");
    // // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// app.UseSession();
app.MapRazorPages();

app.Run();

