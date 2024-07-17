using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using Abstraccion;
using MPP;
using System.Data.SqlClient;
using System.Data;

namespace BLL
{
    public class BLLUsuario : IGestor<BEUsuario>
    {
        public BLLUsuario() { oMPPUsuario = new MPPUsuario(); }

        MPPUsuario oMPPUsuario;

        public BEUsuario VerificarUsuario(string User, string Password)
        {
            return oMPPUsuario.VerificarUsuario(User, Password);
        }

        public bool Borrar(BEUsuario oBEUsuario)
        {
            return oMPPUsuario.Borrar(oBEUsuario);
        }

        public bool Guardar(BEUsuario oBEUsuario)
        {
            return oMPPUsuario.Guardar(oBEUsuario);
        }

        public List<BEUsuario> ListarTodo()
        {
            throw new NotImplementedException();
        }

        public bool VerificarNombreUser(BEUsuario oBEUsuario)
        {
            return oMPPUsuario.VerificarNombreUser(oBEUsuario);
        }
    }
}
