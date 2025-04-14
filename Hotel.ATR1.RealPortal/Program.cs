using Hotel.ATR1.RealPortal.AppMiddlewares;
using Hotel.ATR1.RealPortal.Filters;
using Hotel.ATR1.RealPortal.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers(options => 
//{
//    options.Filters.Add<GlobalExceptionFilter>();
//});

builder.Services
    .AddControllersWithViews(options =>
    {
        options.Filters.Add<GlobalExceptionFilter>();
    })
    .AddViewLocalization();

// Добавляем службы локализации
builder.Services.AddLocalization(option => option.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCulture = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("kk-KZ"),
        new CultureInfo("ru-RU"),
        new CultureInfo("uz-Latn-UZ"),
    };
    options.DefaultRequestCulture = new RequestCulture(culture: "kk-KZ", uiCulture: "kk-KZ");
    options.SupportedCultures = supportedCulture;
    options.SupportedUICultures = supportedCulture;
});

#region Авторизация
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services
    .ConfigureApplicationCookie(option => option.LoginPath = "/Account/Login");
#endregion

#region DI
builder.Services.AddScoped<IMessage, EmailSender>();
builder.Services.AddScoped<IMessage, SmsSender>();
#endregion

#region Logging
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()

    .WriteTo.File("Logs/hotelatr_.txt", rollingInterval: RollingInterval.Minute)

    .WriteTo.MSSqlServer(
    connectionString: connection,
    sinkOptions: new MSSqlServerSinkOptions()
    {
        TableName = "Log"
    })

    .WriteTo.Seq("http://localhost:5341/")

    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();
#endregion




var app = builder.Build();

var supportedCulture = new[] { "en-US", "kk-KZ", "ru-RU", "uz-Latn-UZ" };
var locaizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("kk-KZ")
    .AddSupportedCultures(supportedCulture)
    .AddSupportedUICultures(supportedCulture);

app.UseRequestLocalization(locaizationOptions);


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.Use(async (context, next) => {

//    string str = string.Format("Запрос: {0} {1}",
//        context.Request.Method,
//        context.Request.Path);

//    await next.Invoke();
//});

//app.UseMiddleware<UseLogerRequest>();

app.UseTimeElapsed();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();