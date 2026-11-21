using BarProductBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BarProductBackend.Services.Interfaces;
using BarProductBackend.DTOs;

namespace BarProductBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController:ControllerBase
    {
        private readonly IServicioProducto _service;
        public ProductoController(IServicioProducto service )
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<List<Producto>>> ObtenerTodos()
        {
            var productos = await _service.ObtenerTodosAsync();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> ObtenerPorId(int id)
        {
            var producto = await _service.BuscarPorIdAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Agregar([FromBody] ProductoCreateDTO dto)
        {
            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Precio = dto.Precio,
                Activo = dto.Activo
            };

            await _service.AgregarAsync(producto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = producto.Id },producto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody]ProductoUpdateDTO dto)
        {
            var producto = new Producto
            {
                Id = id,
                Nombre = dto.Nombre,
                Precio = dto.Precio,
                Activo = dto.Activo
            };

            bool actualizado = await _service.ActualizarAsync(producto);
            if (!actualizado)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Eliminar(int id)
        {
            bool eliminado = await _service.EliminarPorIdAsync(id);
            if (!eliminado)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

 