using Inmobiliaria.Dominio;

using Microsoft.EntityFrameworkCore;

namespace Inmobiliaria.Repositorios
{
    public class InmobiliariaContext : DbContext
    {
        public InmobiliariaContext(DbContextOptions<InmobiliariaContext> options)
            : base(options)
        {
        }

        public DbSet<Agente> Agentes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Interes> Intereses { get; set; }
        public DbSet<Propiedad> Propiedades { get; set; }
        public DbSet<Visita> Visitas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Imagen> Imagenes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
