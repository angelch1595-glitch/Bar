using BarProductBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BarProductBackend.DTOs;
using BarProductBackend.Services.Interfaces;
namespace BarProductBackend.Controllers;

public record RegisterRequest(string Usuario, string Email, string Password, string NombreCompleto);
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthServicio _authServicio;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthController( IAuthServicio authServicio, UserManager<ApplicationUser> userManager)
    {
        _authServicio = authServicio;
        _userManager = userManager;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var (respuesta, error) = await _authServicio.LoginAsync(request);

        if (respuesta == null)
        {
            return Unauthorized(new { mensaje = error });
        }

        return Ok(respuesta);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Usuario,
            Email = request.Email,
            NombreCompleto = request.NombreCompleto
        };

        var resultado = await _userManager.CreateAsync(user, request.Password);
        if (!resultado.Succeeded)
        {
            return BadRequest(new
            {
                errores = resultado.Errors.Select(e => e.Description)
            });
        }
        // El rol lo decide el servidor, nunca el cliente 
        await _userManager.AddToRoleAsync(user, "Usuario");
        return Ok(new
        {
            mensaje = "Usuario registrado correctamente",
            usuario = user.UserName
        });
    }
}