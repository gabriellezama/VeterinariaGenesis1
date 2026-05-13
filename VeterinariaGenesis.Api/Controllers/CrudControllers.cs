using Microsoft.AspNetCore.Mvc;
using VeterinariaGenesis.Domain.Entities;
using VeterinariaGenesis.Infrastructure.Repositories;

namespace VeterinariaGenesis.Api.Controllers
{
    [ApiController]
    [Route("api/Clientes")]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteRepository _repo;
        public ClientesController(ClienteRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cliente cliente)
        {
            cliente.Id = Guid.NewGuid();
            await _repo.CreateAsync(cliente);
            return Ok(cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Cliente cliente)
        {
            await _repo.UpdateAsync(id, cliente);
            return Ok(cliente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repo.DeleteAsync(id);
            return Ok();
        }
    }

    [ApiController]
    [Route("api/Mascotas")]
    public class MascotasController : ControllerBase
    {
        private readonly MascotaRepository _repo;
        public MascotasController(MascotaRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Mascota mascota)
        {
            mascota.Id = Guid.NewGuid();
            await _repo.CreateAsync(mascota);
            return Ok(mascota);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Mascota mascota)
        {
            await _repo.UpdateAsync(id, mascota);
            return Ok(mascota);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repo.DeleteAsync(id);
            return Ok();
        }
    }

    [ApiController]
    [Route("api/Trabajadores")]
    public class TrabajadoresController : ControllerBase
    {
        private readonly TrabajadorRepository _repo;
        public TrabajadoresController(TrabajadorRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Trabajador trabajador)
        {
            trabajador.Id = Guid.NewGuid();
            await _repo.CreateAsync(trabajador);
            return Ok(trabajador);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Trabajador trabajador)
        {
            await _repo.UpdateAsync(id, trabajador);
            return Ok(trabajador);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repo.DeleteAsync(id);
            return Ok();
        }
    }

    [ApiController]
    [Route("api/Productos")]
    public class ProductosController : ControllerBase
    {
        private readonly ProductoRepository _repo;
        public ProductosController(ProductoRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Producto producto)
        {
            producto.Id = Guid.NewGuid();
            await _repo.CreateAsync(producto);
            return Ok(producto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Producto producto)
        {
            await _repo.UpdateAsync(id, producto);
            return Ok(producto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repo.DeleteAsync(id);
            return Ok();
        }
    }

    [ApiController]
    [Route("api/Facturas")]
    public class FacturasController : ControllerBase
    {
        private readonly FacturaRepository _facturaRepo;
        private readonly ProductoRepository _productoRepo;

        public FacturasController(FacturaRepository facturaRepo, ProductoRepository productoRepo)
        {
            _facturaRepo = facturaRepo;
            _productoRepo = productoRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _facturaRepo.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Factura factura)
        {
            try 
            {
                factura.Id = Guid.NewGuid();
                // Asegurar que los IDs de los detalles coincidan con la factura
                foreach(var d in factura.Detalles) d.FacturaId = factura.Id;

                // Actualizar stock de cada producto de forma segura
                foreach (var detalle in factura.Detalles)
                {
                    try 
                    {
                        var prod = await _productoRepo.GetByIdAsync(detalle.ProductoId);
                        if (prod != null)
                        {
                            decimal nuevoStock = prod.Stock - detalle.Cantidad;
                            await _productoRepo.UpdateStockAsync(prod.Id, nuevoStock);
                        }
                    }
                    catch (Exception exStock)
                    {
                        Console.WriteLine($"Error actualizando stock para {detalle.ProductoId}: {exStock.Message}");
                        // Continuamos con el siguiente producto para no bloquear la factura
                    }
                }

                await _facturaRepo.CreateAsync(factura);
                return Ok(factura);
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, $"Error al procesar factura: {msg}");
            }
        }
    }

    [ApiController]
    [Route("api/Proveedores")]
    public class ProveedoresController : ControllerBase
    {
        private readonly ProveedorRepository _repo;
        public ProveedoresController(ProveedorRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Proveedor proveedor)
        {
            proveedor.Id = Guid.NewGuid();
            await _repo.CreateAsync(proveedor);
            return Ok(proveedor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Proveedor proveedor)
        {
            await _repo.UpdateAsync(id, proveedor);
            return Ok(proveedor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repo.DeleteAsync(id);
            return Ok();
        }
    }
}
