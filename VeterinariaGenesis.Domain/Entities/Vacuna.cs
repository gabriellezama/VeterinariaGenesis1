using System;

namespace VeterinariaGenesis.Domain.Entities
{
    public class Vacuna : EventoMedico
    {
        public string ProductoAplicado { get; set; } = string.Empty;
        public string Lote { get; set; } = string.Empty;
        public DateTime? ProximaDosis { get; set; }
        
        public Vacuna()
        {
            Tipo = Enums.TipoEventoMedico.Vacuna;
        }
    }
}
