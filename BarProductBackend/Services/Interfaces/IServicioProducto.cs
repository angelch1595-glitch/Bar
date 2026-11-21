using BarProductBackend.Models;
namespace BarProductBackend.Services.Interfaces
{
    public interface IServicioProducto
    {
        Task<List<Producto>> ObtenerTodosAsync();
        Task<Producto?> BuscarPorIdAsync(int id);
        Task<Producto> AgregarAsync(Producto producto);
        Task<bool> ActualizarAsync(Producto producto);
        Task<bool> EliminarPorIdAsync(int id);


    }
}
