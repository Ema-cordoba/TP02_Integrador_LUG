using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEEmpleado : Entidad
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public DateTime FechaIngreso { get; set; }
        public override string ToString()
        {
            return Nombre + " " + Apellido;
        }
    }
}
