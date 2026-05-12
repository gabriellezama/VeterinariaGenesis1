using System;

namespace VeterinariaGenesis.Domain.Entities
{
    public class Cirugia : EventoMedico
    {
        public string TipoAnestesia { get; set; } = string.Empty;
        public string ReportePostOperatorio { get; set; } = string.Empty;
        public bool ConsentimientoInformadoDigital { get; set; }
        
        public Cirugia()
        {
            Tipo = Enums.TipoEventoMedico.Cirugia;
        }
    }
}
