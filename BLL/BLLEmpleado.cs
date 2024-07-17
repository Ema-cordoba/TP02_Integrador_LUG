using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstraccion;
using MPP;

namespace BLL
{
    public class BLLEmpleado : IGestor<BEEmpleado>
    {
        public BLLEmpleado() { oMPPEmpleado = new MPPEmpleado(); }

        MPPEmpleado oMPPEmpleado;

        public bool Borrar(BEEmpleado oBEEmpleado)
        {
            return oMPPEmpleado.Borrar(oBEEmpleado);
        }

        public bool Guardar(BEEmpleado oBEEmpleado)
        {
            return oMPPEmpleado.Guardar(oBEEmpleado);
        }

        public List<BEEmpleado> ListarTodo()
        {
            return oMPPEmpleado.ListarTodo();
        }


        public List<BEVenta> ListarVentasEmpleado(BEEmpleado oBEEmpleado)
        {
            return oMPPEmpleado.ListarVentasEmpleado(oBEEmpleado);
        }


        public List<BEEmpleado> listarEmpleadosSinUsuario()
        {
            return oMPPEmpleado.listarEmpleadosSinUsuario();
        }
        public BEEmpleado ListarEmpleadoMasVentas()
        {
            return oMPPEmpleado.ListarEmpleadoMasVentas();
        }
        public bool obtenerTotalVentas(BEEmpleado oBEEmpleado)
        {
            return oMPPEmpleado.obtenerTotalVentas(oBEEmpleado);
        }
    }
}
