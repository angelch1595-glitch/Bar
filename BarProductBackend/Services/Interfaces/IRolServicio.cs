using Microsoft.AspNetCore.Identity;

namespace BarProductBackend.Services.Interfaces;

public interface IRolServicio
{
    Task<IdentityResult> CrearAsync(string nombre);
    Task<IdentityResult> AsignarAsync(string usuarioId, string nombreRol);
}