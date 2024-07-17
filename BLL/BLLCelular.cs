using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using Abstraccion;
using MPP;

namespace BLL
{
    public class BLLCelular : BLLProducto , IGestor<BECelular>
    {
        public BLLCelular() { oMPPCelular = new MPPCelular(); }

        MPPCelular oMPPCelular;


        public double obtenerDescuento(double Total)
        {
            double descuento = Total * 25 / 100;
            return descuento;
        }
        public bool Borrar(BECelular oBECelular)
        {
            return oMPPCelular.Borrar(oBECelular);
        }

        public bool Guardar(BECelular oBECelular)
        {
            return oMPPCelular.Guardar(oBECelular);
        }

        public List<BECelular> ListarTodo()
        {
            return oMPPCelular.ListarTodo();
        }
    }
}
