using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEUsuario : Entidad
    {
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public BEEmpleado Empleado { get; set; }
        public bool Administrador { get; set; } 
    }
}
