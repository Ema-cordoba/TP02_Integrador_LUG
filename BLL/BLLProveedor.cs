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
    public class BLLProveedor : IGestor<BEProveedor>
    {

        public BLLProveedor() { oMPPProveedor = new MPPProveedor(); }

        MPPProveedor oMPPProveedor;

        public bool Borrar(BEProveedor oBEProveedor)
        {
            return oMPPProveedor.Borrar(oBEProveedor);
        }

        public bool Guardar(BEProveedor oBEProveedpr)
        {
            return oMPPProveedor.Guardar(oBEProveedpr);
        }

        public List<BEProveedor> ListarTodo()
        {
            return oMPPProveedor.ListarTodo();
        }
    }
}
