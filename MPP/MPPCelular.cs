using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
using Abstraccion;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace MPP
{
    public class MPPCelular : IGestor<BECelular>
    {

        public MPPCelular() { oData = new Data(); }

        Data oData;

        ArrayList AL;

        public bool Borrar(BECelular oBECelular)
        {
            AL = new ArrayList();
            if (obtenerVentasProd(oBECelular) == false)
            {
                string Consulta = "s_Producto_Baja";

                SqlParameter sqlparam1 = new SqlParameter("@CodProducto", oBECelular.Codigo);
                sqlparam1.SqlDbType = SqlDbType.Int;
                AL.Add(sqlparam1);

                return oData.Escribir(Consulta, AL);
            }
            else
            {
                return false;
            }
        }


        public bool obtenerVentasProd(BECelular oBECelular)
        {
            oData = new Data();
            return oData.LeerScalar("SELECT COUNT(DETALLE_VENTA.CodProducto) FROM DETALLE_VENTA WHERE DETALLE_VENTA.CodProducto = '" + oBECelular.Codigo + "'");
        }

        public bool Guardar(BECelular oBECelular)
        {
            AL = new ArrayList();
            string Consulta = string.Empty;
            if (oBECelular.Codigo == 0)
            {
                Consulta = "s_Producto_Alta";

                SqlParameter sqlparam1 = new SqlParameter("@Nombre", oBECelular.Nombre);
                sqlparam1.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlparam1);

                SqlParameter sqlparam2 = new SqlParameter("@Descripcion", oBECelular.Descripcion);
                sqlparam2.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlparam2);

                SqlParameter sqlparam3 = new SqlParameter("@Marca", oBECelular.Marca);
                sqlparam3.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlparam3);

                SqlParameter sqlparam4 = new SqlParameter("@Tipo", oBECelular.Tipo);
                sqlparam4.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlparam4);

                SqlParameter sqlparam5 = new SqlParameter("@Precio", oBECelular.Precio);
                sqlparam5.SqlDbType = SqlDbType.Money;
                AL.Add(sqlparam5);

                SqlParameter sqlparam6 = new SqlParameter("@Stock", oBECelular.Stock);
                sqlparam6.SqlDbType = SqlDbType.Int;
                AL.Add(sqlparam6);

                SqlParameter sqlparam7 = new SqlParameter("@CodProveedor", oBECelular.Proveedor.Codigo);
                sqlparam7.SqlDbType = SqlDbType.Int;
                AL.Add(sqlparam7);

                return oData.Escribir(Consulta, AL);
            }
            else
            {
                Consulta = "s_Producto_Modificar";

                SqlParameter sqlparam1 = new SqlParameter("@CodProd", oBECelular.Codigo);
                sqlparam1.SqlDbType = SqlDbType.Int;
                AL.Add(sqlparam1);

                SqlParameter sqlparam2 = new SqlParameter("@Nombre", oBECelular.Nombre);
                sqlparam2.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlparam2);

                SqlParameter sqlparam3 = new SqlParameter("@Descripcion", oBECelular.Descripcion);
                sqlparam3.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlparam3);

                SqlParameter sqlparam4 = new SqlParameter("@Marca", oBECelular.Marca);
                sqlparam4.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlparam4);

                SqlParameter sqlparam5 = new SqlParameter("@Tipo", oBECelular.Tipo);
                sqlparam5.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlparam5);

                SqlParameter sqlparam6 = new SqlParameter("@Precio", oBECelular.Precio);
                sqlparam6.SqlDbType = SqlDbType.Money;
                AL.Add(sqlparam6);

                SqlParameter sqlparam7 = new SqlParameter("@Stock", oBECelular.Stock);
                sqlparam7.SqlDbType = SqlDbType.Int;
                AL.Add(sqlparam7);

                SqlParameter sqlparam8 = new SqlParameter("@CodProveedor", oBECelular.Proveedor.Codigo);
                sqlparam8.SqlDbType = SqlDbType.Int;
                AL.Add(sqlparam8);

                return oData.Escribir(Consulta, AL);
            }
        }

        public List<BECelular> ListarTodo()
        {
            List<BECelular> listaProdCel = new List<BECelular>();
            AL = new ArrayList();

            SqlParameter sqlparam1 = new SqlParameter("@Tipo", "Celular");
            sqlparam1.SqlDbType = SqlDbType.NVarChar;
            AL.Add(sqlparam1);

            DataTable dTable = oData.Leer("s_Producto_ListarT", AL);
            if (dTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dTable.Rows)
                {
                    BECelular oBECelular = new BECelular();
                    oBECelular.Codigo = Convert.ToInt32(dr["Codigo"]);
                    oBECelular.Nombre = Convert.ToString(dr["Nombre"]);
                    oBECelular.Descripcion = Convert.ToString(dr["Descripcion"]);
                    oBECelular.Marca = Convert.ToString(dr["Marca"]);
                    oBECelular.Tipo = Convert.ToString(dr["Tipo"]);
                    oBECelular.Precio = Convert.ToDouble(dr["Precio"]);
                    oBECelular.Stock = Convert.ToInt32(dr["Stock"]);

                    BEProveedor oBEProveedor = new BEProveedor();
                    oBEProveedor.Codigo = Convert.ToInt32(dr["CodProveedor"]);
                    oBEProveedor.RazonSocial = Convert.ToString(dr["RazonSocial"]);
                    oBEProveedor.CUIT = Convert.ToString(dr["CUIT"]);

                    oBECelular.Proveedor = oBEProveedor;

                    listaProdCel.Add(oBECelular);
                }
                return listaProdCel;
            }
            else
            {
                return null;
            }
        }
    }
}
