using BarProductBackend.DTOs;
using BarProductBackend.Models;
using BarProductBackend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BarProductBackend.Services;

public class AuthServicio : IAuthServicio
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenServicio _tokenServicio;

    public AuthServicio(
        UserManager<ApplicationUser> userManager, ITokenServicio tokenServicio)
    {
        _userManager = userManager;
        _tokenServicio = tokenServicio;
    }

    public async Task<(LoginRespuesta? Respuesta, string? Error)> LoginAsync(LoginRequest request)
    {
        var usuario = await _userManager.FindByNameAsync(request.Usuario)
                   ?? await _userManager.FindByEmailAsync(request.Usuario);

        if (usuario == null)
        {
            return (null, "Usuario o contraseña incorrectos");
        }

        if (await _userManager.IsLockedOutAsync(usuario))
        {
            return (null, "Cuenta bloqueada temporalmente. Intenta más tarde.");
        }

        if (!await _userManager.CheckPasswordAsync(usuario, request.Password))
        {
            await _userManager.AccessFailedAsync(usuario);
            return (null, "Usuario o contraseña incorrectos");
        }

        await _userManager.ResetAccessFailedCountAsync(usuario);

        var roles = await _userManager.GetRolesAsync(usuario);
        var (token, expira) = _tokenServicio.Generar(usuario, roles);

        var respuesta = new LoginRespuesta(
            token,
            expira,
            usuario.UserName ?? string.Empty,
            roles
        );

        return (respuesta, null);
    }
}