using BarProductBackend.Models;
using BarProductBackend.Repositories.Interfaces;
using BarProductBackend.Data;
using Microsoft.EntityFrameworkCore;
namespace BarProductBackend.Repositories
{
    public class SQL_ProductoRepositotio: IProducto
    {
        private readonly BarDbContext _db;
        public SQL_ProductoRepositotio(BarDbContext db) {
            _db = db;
        }

        //Obtener todos los productos
        public async Task<List<Producto>> ObtenerTodosAsync() {
            return await _db.Producto.AsNoTracking().ToListAsync();
        }

        // Buscar por ID
        public async Task<Producto?> BuscarPorIdAsync(int id) {
            return await _db.Producto
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        // Agregar un producto
        public async Task<Producto> AgregarAsync(Producto producto) {
            _db.Producto.Add(producto);
            await _db.SaveChangesAsync();
            return producto;

        }

        // Actualizar un producto
        public async  Task<bool> ActualizarAsync(Producto producto)
        {
            var productoExiste = await _db.Producto.FindAsync(producto.Id);
            if (productoExiste == null) { 
                return false;
            }
            productoExiste.Nombre= producto.Nombre;
            productoExiste.Precio = producto.Precio;
            productoExiste.Activo= producto.Activo;
            await _db.SaveChangesAsync();
            return true;
        }

        // Eliminar un producto
        public async Task<bool> EliminarPorIdAsync(int id) {
            var productoExiste = await _db.Producto.FindAsync(id);
            if (productoExiste == null)
            {
                return false;
            }
            _db.Producto.Remove(productoExiste);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
