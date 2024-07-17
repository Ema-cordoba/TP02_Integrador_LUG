using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEVenta : Entidad
    {
        public BEEmpleado Empleado { get; set; }
        public BECliente Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public double Total { get; set; }
        public double Descuento { get; set; }
        public List<BEProducto> Productos;
    }
}

