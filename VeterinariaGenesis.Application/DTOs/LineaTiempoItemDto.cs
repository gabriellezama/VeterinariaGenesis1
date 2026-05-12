using System;

namespace VeterinariaGenesis.Application.DTOs
{
    public class LineaTiempoItemDto
    {
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoEvento { get; set; } = string.Empty; // Vacuna, Cirugia, Examen
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string MedicoResponsable { get; set; } = string.Empty;
        public string DetallesExtras { get; set; } = string.Empty; // JSON o cadena formatada con info específica
        public string Icono { get; set; } = string.Empty;
        public string ColorClase { get; set; } = string.Empty;
    }
}
