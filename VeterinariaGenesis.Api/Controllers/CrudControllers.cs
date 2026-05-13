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
            factura.Id = Guid.NewGuid();

            // Actualizar stock de cada producto
            var productos = await _productoRepo.GetAllAsync();
            foreach (var detalle in factura.Detalles)
            {
                var prod = productos.FirstOrDefault(p => p.Id == detalle.ProductoId);
                if (prod != null)
                {
                    await _productoRepo.UpdateStockAsync(prod.Id, prod.Stock - detalle.Cantidad);
                }
            }

            await _facturaRepo.CreateAsync(factura);
            return Ok(factura);
        }
    }
}
