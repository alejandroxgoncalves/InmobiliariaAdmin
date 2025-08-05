using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inmobiliaria.Dominio
{
    public class Interes
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public string TipoPropiedad { get; set; } 
        public decimal ValorMaximo { get; set; }
        public string Observaciones { get; set; }

        // relacion con Cliente
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
    }
}
