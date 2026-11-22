using BarProductBackend.DTOs;
using BarProductBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarProductBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class RolesController : ControllerBase
{
    private readonly IRolServicio _rolServicio;

    public RolesController(IRolServicio rolServicio)
    {
        _rolServicio = rolServicio;
    }

    [HttpPost]
    public async Task<IActionResult> Crear(CrearRolRequest request)
    {
        var resultado = await _rolServicio.CrearAsync(request.Nombre);

        if (!resultado.Succeeded)
        {
            return BadRequest(new
            {
                errores = resultado.Errors.Select(error => error.Description)
            });
        }

        return StatusCode(201, new
        {
            mensaje = "Rol creado correctamente",
            nombre = request.Nombre.Trim()
        });
    }
    [HttpPost("~/api/usuarios/{id}/roles")]
    public async Task<IActionResult> Asignar(string id, AsignarRolRequest request)
    {
        var resultado = await _rolServicio.AsignarAsync(id, request.Nombre);

        if (!resultado.Succeeded)
        {
            var respuesta = new
            {
                errores = resultado.Errors.Select(e => e.Description)
            };

            if (resultado.Errors.Any(e =>
                e.Code == "UsuarioNoEncontrado" || e.Code == "RolNoEncontrado"))
            {
                return NotFound(respuesta);
            }

            return BadRequest(respuesta);
        }

        return NoContent();
    }
}