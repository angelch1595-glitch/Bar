using BarProductBackend.Models;
namespace BarProductBackend.Repositories.Interfaces
{
    public interface IProducto
    {
        Task<List<Producto>> ObtenerTodosAsync();
        Task<Producto?> BuscarPorIdAsync(int id);
        Task<Producto> AgregarAsync(Producto producto);
        Task<bool> ActualizarAsync(Producto producto);
        Task<bool> EliminarPorIdAsync(int id);


    }
}
