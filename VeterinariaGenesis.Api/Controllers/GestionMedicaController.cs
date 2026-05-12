using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VeterinariaGenesis.Application.Services;

namespace VeterinariaGenesis.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GestionMedicaController : ControllerBase
    {
        private readonly IHistorialMedicoService _historialService;

        public GestionMedicaController(IHistorialMedicoService historialService)
        {
            _historialService = historialService;
        }

        [HttpGet("historial/{mascotaId}")]
        public async Task<IActionResult> ObtenerHistorialClinico(Guid mascotaId)
        {
            try
            {
                var historial = await _historialService.ObtenerLineaTiempoMascotaAsync(mascotaId);
                return Ok(historial);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
        }
    }
}
