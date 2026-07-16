using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeterinariaGenesis.Domain.Entities;
using VeterinariaGenesis.Infrastructure.Repositories;

namespace VeterinariaGenesis.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly PedidoRepository _pedidoRepo;
        private readonly ProductoRepository _productoRepo;

        public PedidosController(PedidoRepository pedidoRepo, ProductoRepository productoRepo)
        {
            _pedidoRepo = pedidoRepo;
            _productoRepo = productoRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _pedidoRepo.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var pedido = await _pedidoRepo.GetByIdAsync(id);
            if (pedido == null) return NotFound();
            return Ok(pedido);
        }

        [HttpPost]
        [AllowAnonymous] // Permite que usuarios sin autenticación envíen compras web
        public async Task<IActionResult> Create([FromBody] Pedido pedido)
        {
            try 
            {
                pedido.Id = Guid.NewGuid();
                pedido.FechaPedido = DateTime.Now;
                pedido.Estado = "Pendiente";

                // Validar y descontar stock
                foreach (var detalle in pedido.Detalles)
                {
                    var prod = await _productoRepo.GetByIdAsync(detalle.ProductoId);
                    if (prod != null)
                    {
                        if (prod.Stock < detalle.Cantidad)
                        {
                            return BadRequest($"Stock insuficiente para el producto {prod.Nombre}. Disponible: {prod.Stock}");
                        }
                        decimal nuevoStock = prod.Stock - detalle.Cantidad;
                        await _productoRepo.UpdateStockAsync(prod.Id, nuevoStock);
                    }
                }

                await _pedidoRepo.CreateAsync(pedido);
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, $"Error al procesar pedido: {msg}");
            }
        }

        [HttpPut("{id}/estado")]
        public async Task<IActionResult> UpdateEstado(Guid id, [FromBody] string nuevoEstado)
        {
            var pedido = await _pedidoRepo.GetByIdAsync(id);
            if (pedido == null) return NotFound("Pedido no encontrado");

            await _pedidoRepo.UpdateEstadoAsync(id, nuevoEstado);
            return Ok(new { Message = "Estado actualizado con éxito" });
        }
    }
}
