using BarProductBackend.DTOs;

namespace BarProductBackend.Services.Interfaces;
public interface IAuthServicio
{
    Task<(LoginRespuesta? Respuesta, string? Error)> LoginAsync(LoginRequest request);
}