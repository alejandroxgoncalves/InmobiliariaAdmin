using Microsoft.EntityFrameworkCore;
 //  esto para usar InmobiliariaContext
using Inmobiliaria.Repositorios;


var builder = WebApplication.CreateBuilder(args);

//   DbContext usando la cadena de conexion del appsettings.json
builder.Services.AddDbContext<InmobiliariaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//  servicios MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuracion del pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
