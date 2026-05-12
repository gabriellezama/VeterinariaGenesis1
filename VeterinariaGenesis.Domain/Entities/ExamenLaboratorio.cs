using System;
using System.Collections.Generic;

namespace VeterinariaGenesis.Domain.Entities
{
    public class ExamenLaboratorio : EventoMedico
    {
        public string TipoExamen { get; set; } = string.Empty;
        public string Resultados { get; set; } = string.Empty;
        public List<string> ArchivosAdjuntosUrls { get; set; } = new List<string>();

        public ExamenLaboratorio()
        {
            Tipo = Enums.TipoEventoMedico.ExamenLaboratorio;
        }
    }
}
