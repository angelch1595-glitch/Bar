
using BarProductBackend.Models;
using BarProductBackend.Services.Interfaces;
using BarProductBackend.Repositories.Interfaces;
namespace BarProductBackend.Services
{
    public class ServicioProducto:IServicioProducto
    {
        private readonly IProducto _repo;
        public ServicioProducto(IProducto repo  )
        {
            _repo = repo;
        }

        public  Task<List<Producto>> ObtenerTodosAsync() { 
            return _repo.ObtenerTodosAsync();
        }
        public Task<Producto?> BuscarPorIdAsync(int id) {
            return _repo.BuscarPorIdAsync(id);
        }
        public Task<Producto> AgregarAsync(Producto producto) {
            return _repo.AgregarAsync(producto);
        }
        public Task<bool> ActualizarAsync(Producto producto) { 
            return _repo.ActualizarAsync(producto);
        }
        public Task<bool> EliminarPorIdAsync(int id) {
            return _repo.EliminarPorIdAsync(id);
        }
    }
}
