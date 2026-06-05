using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VeterinariaGenesis.Domain.Entities;
using VeterinariaGenesis.Infrastructure.Repositories;

namespace VeterinariaGenesis.Api.Controllers
{
    [ApiController]
    [Route("api/Citas")]
    public class CitasController : ControllerBase
    {
        private readonly CitaRepository _citaRepo;
        private readonly NotificacionWhatsappLogRepository _logRepo;
        private readonly ClienteRepository _clienteRepo;
        private readonly MascotaRepository _mascotaRepo;
        private readonly TrabajadorRepository _trabajadorRepo;

        public CitasController(
            CitaRepository citaRepo,
            NotificacionWhatsappLogRepository logRepo,
            ClienteRepository clienteRepo,
            MascotaRepository mascotaRepo,
            TrabajadorRepository trabajadorRepo)
        {
            _citaRepo = citaRepo;
            _logRepo = logRepo;
            _clienteRepo = clienteRepo;
            _mascotaRepo = mascotaRepo;
            _trabajadorRepo = trabajadorRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _citaRepo.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cita cita)
        {
            cita.Id = Guid.NewGuid();
            cita.NotificadoWhatsapp = false;
            cita.FechaNotificacion = null;
            await _citaRepo.CreateAsync(cita);
            return Ok(cita);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Cita cita)
        {
            await _citaRepo.UpdateAsync(id, cita);
            return Ok(cita);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _citaRepo.DeleteAsync(id);
            return Ok();
        }

        [HttpGet("notificaciones-log")]
        public async Task<IActionResult> GetNotificationsLog()
            => Ok(await _logRepo.GetAllAsync());

        [HttpDelete("notificaciones-log/{id}")]
        public async Task<IActionResult> DeleteNotificationLog(Guid id)
        {
            await _logRepo.DeleteAsync(id);
            return Ok();
        }

        [HttpDelete("notificaciones-log")]
        public async Task<IActionResult> ClearNotificationsLog()
        {
            await _logRepo.DeleteAllAsync();
            return Ok();
        }

        [HttpPost("{id}/notificar-manual")]
        public async Task<IActionResult> SendManualNotification(Guid id)
        {
            var cita = await _citaRepo.GetByIdAsync(id);
            if (cita == null)
                return NotFound("Cita no encontrada.");

            var clientes = await _clienteRepo.GetAllAsync();
            var cliente = clientes.Find(c => c.Id == cita.ClienteId);
            if (cliente == null)
                return NotFound("Cliente de la cita no encontrado.");

            var mascotas = await _mascotaRepo.GetAllAsync();
            var mascota = mascotas.Find(m => m.Id == cita.MascotaId);
            string mascotaNombre = mascota?.Nombre ?? "tu mascota";

            var trabajadores = await _trabajadorRepo.GetAllAsync();
            var trabajador = trabajadores.Find(t => t.Id == cita.TrabajadorId);
            string veterinarioNombre = trabajador != null ? $"{trabajador.Nombres} {trabajador.Apellidos}" : "Veterinario Asignado";

            string phone = string.IsNullOrEmpty(cliente.Telefono) ? "8888-8888" : cliente.Telefono;
            string clienteNombre = $"{cliente.Nombres} {cliente.Apellidos}".Trim();
            if (string.IsNullOrEmpty(clienteNombre)) clienteNombre = cliente.DisplayNombres;

            // Formatear mensaje
            string mensaje = $"Hola {clienteNombre}, te recordamos que {mascotaNombre} tiene una cita programada en Veterinaria Génesis el {cita.FechaHora:dd/MM/yyyy} a las {cita.FechaHora:hh:mm tt} con el Dr. {veterinarioNombre} para {cita.Motivo}. Dir: Semaforos de la cañada 10 vara al este. Tel: 8878-2492.";

            var log = new NotificacionWhatsappLog
            {
                Id = Guid.NewGuid(),
                CitaId = cita.Id,
                ClienteNombre = clienteNombre,
                Telefono = phone,
                Mensaje = mensaje,
                FechaEnvio = DateTime.Now,
                EstadoEnvio = "Enviado (Manual)"
            };

            await _logRepo.CreateAsync(log);

            // Actualizar cita
            cita.NotificadoWhatsapp = true;
            cita.FechaNotificacion = DateTime.Now;
            await _citaRepo.UpdateAsync(cita.Id, cita);

            return Ok(log);
        }
    }
}
