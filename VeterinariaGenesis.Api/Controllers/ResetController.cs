using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using VeterinariaGenesis.Domain.Entities;
using VeterinariaGenesis.Infrastructure.Data;

namespace VeterinariaGenesis.Api.Controllers
{
    [ApiController]
    [Route("api/Reset")]
    public class ResetController : ControllerBase
    {
        private readonly MongoDbContext _context;

        public ResetController(MongoDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Elimina todas las facturas, gastos, citas, clientes, mascotas,
        /// eventos médicos y logs de WhatsApp — dejando trabajadores y
        /// productos intactos (para no perder la nómina ni el catálogo).
        /// </summary>
        [HttpDelete("datos-operativos")]
        public async Task<IActionResult> ResetDatosOperativos()
        {
            // 1. Eliminar Facturas
            await _context.Facturas.DeleteManyAsync(FilterDefinition<Factura>.Empty);

            // 2. Eliminar Gastos
            await _context.Gastos.DeleteManyAsync(FilterDefinition<Gasto>.Empty);

            // 3. Eliminar Citas
            await _context.Citas.DeleteManyAsync(FilterDefinition<Cita>.Empty);

            // 4. Eliminar Eventos Médicos (Historial clínico)
            await _context.EventosMedicos.DeleteManyAsync(FilterDefinition<EventoMedico>.Empty);

            // 5. Eliminar Logs de Notificaciones WhatsApp
            await _context.NotificacionWhatsappLogs.DeleteManyAsync(FilterDefinition<NotificacionWhatsappLog>.Empty);

            // 6. Eliminar Clientes
            await _context.Clientes.DeleteManyAsync(FilterDefinition<Cliente>.Empty);

            // 7. Eliminar Mascotas
            await _context.Mascotas.DeleteManyAsync(FilterDefinition<Mascota>.Empty);

            return Ok(new { mensaje = "Datos operativos eliminados. Trabajadores y catálogo de productos conservados." });
        }

        /// <summary>
        /// Reseteo TOTAL: borra absolutamente todo incluyendo trabajadores y productos.
        /// </summary>
        [HttpDelete("todo")]
        public async Task<IActionResult> ResetTotal()
        {
            await _context.Facturas.DeleteManyAsync(FilterDefinition<Factura>.Empty);
            await _context.Gastos.DeleteManyAsync(FilterDefinition<Gasto>.Empty);
            await _context.Citas.DeleteManyAsync(FilterDefinition<Cita>.Empty);
            await _context.EventosMedicos.DeleteManyAsync(FilterDefinition<EventoMedico>.Empty);
            await _context.NotificacionWhatsappLogs.DeleteManyAsync(FilterDefinition<NotificacionWhatsappLog>.Empty);
            await _context.Clientes.DeleteManyAsync(FilterDefinition<Cliente>.Empty);
            await _context.Mascotas.DeleteManyAsync(FilterDefinition<Mascota>.Empty);
            await _context.Trabajadores.DeleteManyAsync(FilterDefinition<Trabajador>.Empty);
            await _context.Productos.DeleteManyAsync(FilterDefinition<Producto>.Empty);
            await _context.Proveedores.DeleteManyAsync(FilterDefinition<Proveedor>.Empty);

            return Ok(new { mensaje = "Reseteo total completado. Base de datos completamente limpia." });
        }
    }
}
