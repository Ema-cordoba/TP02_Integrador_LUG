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
    public class MPPComputadora : IGestor<BEComputadora>
    {
        public MPPComputadora() { oData = new Data(); }

        Data oData;
        ArrayList AL;


        public bool Borrar(BEComputadora oBEComputadora)
        {
            AL = new ArrayList();
            if(obtenerVentasProd(oBEComputadora) == false)
            {
                string consulta = "s_Producto_Baja";
                SqlParameter sqlparam1 = new SqlParameter("@CodProducto", oBEComputadora.Codigo);
                sqlparam1.SqlDbType = SqlDbType.Int;
                AL.Add(sqlparam1);

                return oData.Escribir(consulta, AL);
            }
            else
            {
                return false;
            }
        }



        public bool obtenerVentasProd(BEComputadora oBEComputadora)
        {
            oData = new Data();
            return oData.LeerScalar("SELECT COUNT(DETALLE_VENTA.CodProducto) FROM DETALLE_VENTA WHERE DETALLE_VENTA.CodProducto = '" + oBEComputadora.Codigo + "'");
        }

        public bool Guardar(BEComputadora oBEComputadora)
        {
            AL = new ArrayList();
            string Consulta = string.Empty;
            if (oBEComputadora.Codigo == 0)
            {
                Consulta = "s_Producto_Alta";

                SqlParameter sqlparam1 = new SqlParameter("@Nombre", oBEComputadora.Nombre);
                sqlparam1.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlparam1);

                SqlParameter sqlparam2 = new SqlParameter("@Descripcion", oBEComputadora.Descripcion);
                sqlparam2.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlparam2);

                SqlParameter sqlparam3 = new SqlParameter("@Marca", oBEComputadora.Marca);
                sqlparam3.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlparam3);

                SqlParameter sqlparam4 = new SqlParameter("@Tipo", oBEComputadora.Tipo);
                sqlparam4.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlparam4);

                SqlParameter sqlparam5 = new SqlParameter("@Precio", oBEComputadora.Precio);
                sqlparam5.SqlDbType = SqlDbType.Money;
                AL.Add(sqlparam5);

                SqlParameter sqlparam6 = new SqlParameter("@Stock", oBEComputadora.Stock);
                sqlparam6.SqlDbType = SqlDbType.Int;
                AL.Add(sqlparam6);

                SqlParameter sqlparam7 = new SqlParameter("@CodProveedor", oBEComputadora.Proveedor.Codigo);
                sqlparam7.SqlDbType = SqlDbType.Int;
                AL.Add(sqlparam7);

                return oData.Escribir(Consulta, AL);
            }
            else
            {
                Consulta = "s_Producto_Modificar";

                SqlParameter sqlparam1 = new SqlParameter("@CodProd", oBEComputadora.Codigo);
                sqlparam1.SqlDbType = SqlDbType.Int;
                AL.Add(sqlparam1);

                SqlParameter sqlparam2 = new SqlParameter("@Nombre", oBEComputadora.Nombre);
                sqlparam2.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlparam2);

                SqlParameter sqlparam3 = new SqlParameter("@Descripcion", oBEComputadora.Descripcion);
                sqlparam3.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlparam3);

                SqlParameter sqlparam4 = new SqlParameter("@Marca", oBEComputadora.Marca);
                sqlparam4.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlparam4);

                SqlParameter sqlparam5 = new SqlParameter("@Tipo", oBEComputadora.Tipo);
                sqlparam5.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlparam5);

                SqlParameter sqlparam6 = new SqlParameter("@Precio", oBEComputadora.Precio);
                sqlparam6.SqlDbType = SqlDbType.Money;
                AL.Add(sqlparam6);

                SqlParameter sqlparam7 = new SqlParameter("@Stock", oBEComputadora.Stock);
                sqlparam7.SqlDbType = SqlDbType.Int;
                AL.Add(sqlparam7);

                SqlParameter sqlparam8 = new SqlParameter("@CodProveedor", oBEComputadora.Proveedor.Codigo);
                sqlparam8.SqlDbType = SqlDbType.Int;
                AL.Add(sqlparam8);

                return oData.Escribir(Consulta, AL);
            }
        }

        public List<BEComputadora> ListarTodo()
        {
            List<BEComputadora> listaProdComp = new List<BEComputadora>();
            AL = new ArrayList();

            SqlParameter sqlparam1 = new SqlParameter("@Tipo", "Computadora");
            sqlparam1.SqlDbType = SqlDbType.NVarChar;
            AL.Add(sqlparam1);

            DataTable dTable = oData.Leer("s_Producto_ListarT", AL);
            if (dTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dTable.Rows)
                {
                    BEComputadora oBEComputadora = new BEComputadora();
                    oBEComputadora.Codigo = Convert.ToInt32(dr["Codigo"]);
                    oBEComputadora.Nombre = Convert.ToString(dr["Nombre"]);
                    oBEComputadora.Descripcion = Convert.ToString(dr["Descripcion"]);
                    oBEComputadora.Marca = Convert.ToString(dr["Marca"]);
                    oBEComputadora.Tipo = Convert.ToString(dr["Tipo"]);
                    oBEComputadora.Precio = Convert.ToDouble(dr["Precio"]);
                    oBEComputadora.Stock = Convert.ToInt32(dr["Stock"]);

                    BEProveedor oBEProveedor = new BEProveedor();
                    oBEProveedor.Codigo = Convert.ToInt32(dr["CodProveedor"]);
                    oBEProveedor.RazonSocial = Convert.ToString(dr["RazonSocial"]);
                    oBEProveedor.CUIT = Convert.ToString(dr["CUIT"]);

                    oBEComputadora.Proveedor = oBEProveedor;

                    listaProdComp.Add(oBEComputadora);
                }
                return listaProdComp;
            }
            else
            {
                return null;
            }
        }
    }
}
