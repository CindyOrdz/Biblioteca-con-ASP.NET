using ASP_MVC_MYSQL.Data.DAO;
using ASP_MVC_MYSQL.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Registra el servicio de base de datos en el contenedor de inyección de dependencias como Singleton
var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");

builder.Services.AddSingleton<MySqlConnection>(_ => new MySqlConnection(connectionString));


// Agrega servicios
builder.Services.AddSingleton<LibroDAO>();
builder.Services.AddSingleton<UsuarioDAO>();
builder.Services.AddSingleton<LibroService>();
builder.Services.AddSingleton<UsuarioService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option => {
        option.LoginPath = "/Usuario/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        option.AccessDeniedPath = "/Usuario/Restringido";
        // Configura la cookie como segura,para que siempre se envíe a través de conexiones HTTPS seguras.
        option.Cookie.SecurePolicy = CookieSecurePolicy.Always; 
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

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
