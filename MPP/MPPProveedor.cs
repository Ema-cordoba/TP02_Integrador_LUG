using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using Abstraccion;
using DAL;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace MPP
{
    public class MPPProveedor : IGestor<BEProveedor>
    {

        public MPPProveedor() { oData = new Data(); }

        Data oData;
        ArrayList AL;


        public bool Borrar(BEProveedor oBEProveedor)
        {
            string Consulta = string.Empty;
            AL = new ArrayList();
            if (obtenerProductos_Prov(oBEProveedor) == false)
            {
                Consulta = "s_Proveedor_Baja";

                SqlParameter sqlparam1 = new SqlParameter("@Codigo", oBEProveedor.Codigo);
                sqlparam1.SqlDbType = SqlDbType.Int;

                AL.Add(sqlparam1);
                return oData.Escribir(Consulta, AL);
            }
            else
            {
                return false;
            }
        }

        public bool obtenerProductos_Prov(BEProveedor oBEProveedor)
        {
            oData = new Data();
            return oData.LeerScalar("SELECT COUNT(PRODUCTO.Codigo) FROM PRODUCTO WHERE PRODUCTO.CodProveedor = '" + oBEProveedor.Codigo + "'");
        }

        public bool Guardar(BEProveedor oBEProveedor)
        {
            AL = new ArrayList();
            string Consulta = string.Empty;
            if (oBEProveedor.Codigo == 0)
            {
                Consulta = "s_Proveedor_Alta";

                SqlParameter sqlparam1 = new SqlParameter("@RazonSocial", oBEProveedor.RazonSocial);
                sqlparam1.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlparam1);

                SqlParameter sqlparam2 = new SqlParameter("@CUIT", oBEProveedor.CUIT);
                sqlparam2.SqlDbType = SqlDbType.Char;
                AL.Add(sqlparam2);

                return oData.Escribir(Consulta, AL);
            }
            else
            {
                Consulta = "s_Proveedor_Modificar";

                SqlParameter sqlparam1 = new SqlParameter("@Codigo", oBEProveedor.Codigo);
                sqlparam1.SqlDbType = SqlDbType.Int;
                AL.Add(sqlparam1);

                SqlParameter sqlparam2 = new SqlParameter("@RazonSocial", oBEProveedor.RazonSocial);
                sqlparam2.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlparam2);

                SqlParameter sqlparam3 = new SqlParameter("@CUIT", oBEProveedor.CUIT);
                sqlparam3.SqlDbType = SqlDbType.Char;
                AL.Add(sqlparam3);

                return oData.Escribir(Consulta, AL);
            }
        }

        public List<BEProveedor> ListarTodo()
        {
            List<BEProveedor> listaProveedores = new List<BEProveedor>();
            DataTable dTable = oData.Leer("s_Proveedor_ListarT", null);

            if (dTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dTable.Rows)
                {
                    BEProveedor oBEProveedor = new BEProveedor();
                    oBEProveedor.Codigo = Convert.ToInt32(dr["Codigo"]);
                    oBEProveedor.RazonSocial = Convert.ToString(dr["RazonSocial"]);
                    oBEProveedor.CUIT = Convert.ToString(dr["CUIT"]);
                    listaProveedores.Add(oBEProveedor);
                }
                return listaProveedores;
            }
            else
            {
                return null;
            }
        }
    }
}
