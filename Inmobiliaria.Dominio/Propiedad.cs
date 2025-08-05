using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inmobiliaria.Dominio
{
    public enum TipoOperacion
    {
        Venta,
        Alquiler,
        VentaAlquiler
    }

    public class Propiedad
    {
        public int Id { get; set; }

        public string Titulo { get; set; }
        public string TipoPropiedad { get; set; } 
        public string Descripcion { get; set; }
        public string Direccion { get; set; }

        public double SuperficieTotal { get; set; }
        public double SuperficieConstruida { get; set; }

        public double Valor { get; set; } 

        public int Banios { get; set; }
        
        public int Dormitorios { get; set; }
        public bool TieneGarage { get; set; }

        public int Antiguedad { get; set; }
        public bool Activa { get; set; }

        public TipoOperacion TipoOperacion { get; set; }

        // relaciones
        public ICollection<Visita> Visitas { get; set; }
        public ICollection<Imagen> Imagenes { get; set; }
    }
}
