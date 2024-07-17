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
    public class BLLComputadora : BLLProducto , IGestor<BEComputadora>
    {

        public BLLComputadora() { oMPPComputadora = new MPPComputadora(); }

        MPPComputadora oMPPComputadora;


        public double obtenerDescuento(double Total)
        {
            double descuento = Total * 15 / 100;
            return descuento;
        }

        public bool Borrar(BEComputadora oBEComputadora)
        {
            return oMPPComputadora.Borrar(oBEComputadora);
        }

        public bool Guardar(BEComputadora oBEComputadora)
        {
            return oMPPComputadora.Guardar(oBEComputadora);
        }

        public List<BEComputadora> ListarTodo()
        {
            return oMPPComputadora.ListarTodo();
        }
    }
}
