using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inmobiliaria.Dominio
{
    public enum EstadoVisita
    {
        Agendada,
        Realizada,
        Suspendida
    }

    public class Visita
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public EstadoVisita Estado { get; set; }

        public string Observaciones { get; set; }

        // Relaciones

        public int PropiedadId { get; set; }
        public Propiedad Propiedad { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public int AgenteId { get; set; }
        public Agente Agente { get; set; }
    }
}
