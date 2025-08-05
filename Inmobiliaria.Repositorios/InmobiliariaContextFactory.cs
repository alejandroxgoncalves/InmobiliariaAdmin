using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Inmobiliaria.Repositorios
{
    public class InmobiliariaContextFactory : IDesignTimeDbContextFactory<InmobiliariaContext>
    {
        public InmobiliariaContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../CapaPresentacionAdmin"))

                .AddJsonFile("appsettings.json") // buscar en el startup project
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<InmobiliariaContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new InmobiliariaContext(optionsBuilder.Options);
        }
    }
}
