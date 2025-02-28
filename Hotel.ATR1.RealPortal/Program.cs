using Hotel.ATR1.RealPortal.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services
    .ConfigureApplicationCookie(option => option.LoginPath = "/Account/Login");


builder.Services.AddScoped<IMessage, EmailSender>();
builder.Services.AddScoped<IMessage, SmsSender>();

//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();


//connection = data source=178.89.186.221, 1434;initial catalog=h ......
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





var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
