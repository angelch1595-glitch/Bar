using Microsoft.EntityFrameworkCore;
using BarProductBackend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace BarProductBackend.Data
{
    public class BarDbContext : IdentityDbContext<ApplicationUser> // Hereda la clase, con eso ya sabe hablar con la base de datos
    {
        //Recibe la configuracion y la cadena de conexion
        public BarDbContext(DbContextOptions<BarDbContext> options) : base(options) { }
        public DbSet<Producto> Producto => Set<Producto>(); //Para crear la tabla producto
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);   // NUEVA: siempre primero 
            modelBuilder.Entity<Producto>(
                entidadProducto => {
                    entidadProducto.Property(p => p.Nombre).HasMaxLength(100);
                    entidadProducto.Property(p => p.Precio).HasPrecision(18, 2);
                    }
                );

        }

        
    }
       
}
