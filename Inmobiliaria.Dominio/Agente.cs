using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inmobiliaria.Dominio
{
    public class Agente
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }

        public string Documento { get; set; }

        // navegacion, un agente puede tener muchas visitas asignadas
        public ICollection<Visita> Visitas { get; set; }
    }
}
