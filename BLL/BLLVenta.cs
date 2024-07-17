using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstraccion;
using BE;
using MPP;

namespace BLL
{
    public class BLLVenta : IGestor<BEVenta>
    {
        public BLLVenta() { oMPPVenta = new MPPVenta(); }

        MPPVenta oMPPVenta;

        public bool Borrar(BEVenta obj)
        {
            throw new NotImplementedException();
        }

        public bool Guardar(BEVenta oBEVenta)
        {
            return oMPPVenta.Guardar(oBEVenta);
        }

        public bool RegistrarDetalle_Venta(BEVenta oBEVenta)
        {
            return oMPPVenta.RegistrarDetalle_Venta(oBEVenta);
        }

        public List<BEVenta> ListarTodo()
        {
            return oMPPVenta.ListarTodo();
        }

        public BEVenta ListarVentaMayor()
        {
            return oMPPVenta.ListarVentaMayor();
        }
    }
}
