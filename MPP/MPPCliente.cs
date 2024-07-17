using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Abstraccion;
using BE;
using DAL;

namespace MPP
{
    public class MPPCliente : IGestor<BECliente>
    {

        Data oData;
        ArrayList AL;

        public MPPCliente() 
        {
            oData = new Data();
        }

        public bool Borrar(BECliente oBECliente)
        {
            // Borrar un cliente, si el mismo contiene compras no puede ser eliminado
            AL = new ArrayList();
            string consulta = string.Empty;
            if (obtenerCompras(oBECliente) == false)
            {
                consulta = "s_Cliente_Baja";
                SqlParameter sqlparam1 = new SqlParameter("@CodCliente", oBECliente.Codigo);
                sqlparam1.SqlDbType = SqlDbType.Int;
                AL.Add(sqlparam1);
                return oData.Escribir(consulta, AL);
            }
            else
            {
                return false;
            }
        }


        public bool obtenerCompras(BECliente oBECliente)
        {
            // retorna bool dependiendo del valor de la consulta, true si tiene compras y false si no
            oData = new Data();
            return oData.LeerScalar("Select Count(VENTA.CodCliente) From VENTA where VENTA.CodCliente = '" + oBECliente.Codigo + "'");
        }

        public bool Guardar(BECliente oBECliente)
        {
            AL = new ArrayList();
            string consulta = string.Empty;
            if (oBECliente.Codigo == 0)
            {
                consulta = "s_Cliente_Alta";
                SqlParameter sqlParam1 = new SqlParameter("@NomCliente", oBECliente.Nombre);
                sqlParam1.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlParam1);

                SqlParameter sqlParam2 = new SqlParameter("@ApeCliente", oBECliente.Apellido);
                sqlParam2.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlParam2);

                SqlParameter sqlParam3 = new SqlParameter("@DniCliente", oBECliente.DNI);
                sqlParam3.SqlDbType = SqlDbType.Char;
                AL.Add(sqlParam3);

                SqlParameter sqlParam4 = new SqlParameter("@TelCliente", oBECliente.Telefono);
                sqlParam4.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlParam4);

                SqlParameter sqlParam5 = new SqlParameter("@EmailCliente", oBECliente.Email);
                sqlParam5.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlParam5);

                return oData.Escribir(consulta, AL);
            }
            else
            {
                consulta = "s_Cliente_Modificar";
                SqlParameter sqlParam1 = new SqlParameter("@CodCliente", oBECliente.Codigo);
                sqlParam1.SqlDbType = SqlDbType.Int;
                AL.Add(sqlParam1);

                SqlParameter sqlParam2 = new SqlParameter("@NomCliente", oBECliente.Nombre);
                sqlParam2.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlParam2);

                SqlParameter sqlParam3 = new SqlParameter("@ApeCliente", oBECliente.Apellido);
                sqlParam3.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlParam3);

                SqlParameter sqlParam4 = new SqlParameter("@DniCliente", oBECliente.DNI);
                sqlParam4.SqlDbType = SqlDbType.Char;
                AL.Add(sqlParam4);

                SqlParameter sqlParam5 = new SqlParameter("@TelCliente", oBECliente.Telefono);
                sqlParam5.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlParam5);

                SqlParameter sqlParam6 = new SqlParameter("@EmailCliente", oBECliente.Email);
                sqlParam6.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlParam6);
                return oData.Escribir(consulta, AL);
            }
        }

        public List<BECliente> ListarTodo()
        {
            List<BECliente> listaClientes = new List<BECliente>();
            DataTable dataT = oData.Leer("s_Cliente_ListarT", null);

            if (dataT.Rows.Count > 0)
            {
                foreach (DataRow dr in dataT.Rows)
                {
                    BECliente objBECliente = new BECliente();
                    objBECliente.Codigo = Convert.ToInt32(dr["Codigo"]);
                    objBECliente.Nombre = Convert.ToString(dr["Nombre"]);
                    objBECliente.Apellido = Convert.ToString(dr["Apellido"]);
                    objBECliente.DNI = Convert.ToString(dr["DNI"]);
                    objBECliente.Telefono = Convert.ToString(dr["Telefono"]);
                    objBECliente.Email = Convert.ToString(dr["Email"]);
                    listaClientes.Add(objBECliente);
                }
                return listaClientes;
            }
            else
            {
                return null;
            }
        }


        public List<BEVenta> ListarVentasCliente(BECliente oBECliente)
        {
            List<BEVenta> listaVentasCliente = new List<BEVenta>();
            AL = new ArrayList();
            DataTable dTable = new DataTable();
            string Consulta = "s_Cliente_ListarV";

            SqlParameter sqlparam1 = new SqlParameter("@CodCliente", oBECliente.Codigo);
            sqlparam1.SqlDbType = SqlDbType.Int;
            AL.Add(sqlparam1);

            dTable = oData.Leer(Consulta, AL);

            if (dTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dTable.Rows)
                {
                    BEVenta oBEVenta = new BEVenta();
                    oBEVenta.Codigo = Convert.ToInt32(dr["CodVenta"]);
                    BEEmpleado oBEEmpleado = new BEEmpleado();
                    oBEEmpleado.Codigo = Convert.ToInt32(dr["CodEmpleado"]);
                    oBEEmpleado.Nombre = Convert.ToString(dr["NEmpleado"]);
                    oBEEmpleado.Apellido = Convert.ToString(dr["AEmpleado"]);
                    oBEEmpleado.Dni = Convert.ToString(dr["DniEmpleado"]);
                    oBEEmpleado.FechaIngreso = Convert.ToDateTime(dr["FechaIngreso"]);
                    oBEVenta.Empleado = oBEEmpleado;
                    oBEVenta.Cliente = oBECliente;
                    oBEVenta.Fecha = Convert.ToDateTime(dr["Fecha"]);
                    oBEVenta.Total = Convert.ToDouble(dr["Total"]);
                    oBEVenta.Descuento = Convert.ToDouble(dr["Descuento"]);

                    AL.Clear();
                    SqlParameter sqlparam2 = new SqlParameter("@CodVenta", oBEVenta.Codigo);
                    sqlparam2.SqlDbType = SqlDbType.Int;
                    AL.Add(sqlparam2);
                    List<BEProducto> listaProductos = new List<BEProducto>();
                    DataTable dTable2 = oData.Leer("s_Venta_ListarD", AL);

                    if (dTable2.Rows.Count > 0)
                    {
                        foreach (DataRow dr2 in dTable2.Rows)
                        {
                            if (dr2["Tipo"].ToString() == "Computacion")
                            {
                                BEComputadora oBEComputadora = new BEComputadora();
                                oBEComputadora.Codigo = Convert.ToInt32(dr2["CodProducto"]);
                                oBEComputadora.Nombre = Convert.ToString(dr2["Nombre"]);
                                oBEComputadora.Tipo = Convert.ToString(dr2["Tipo"]);
                                oBEComputadora.Stock = Convert.ToInt32(dr2["Cantidad"]);
                                oBEComputadora.Precio = Convert.ToDouble(dr2["Precio"]);

                                listaProductos.Add(oBEComputadora);
                            }
                            else
                            {
                                BECelular oBECelular = new BECelular();
                                oBECelular.Codigo = Convert.ToInt32(dr2["CodProducto"]);
                                oBECelular.Nombre = Convert.ToString(dr2["Nombre"]);
                                oBECelular.Tipo = Convert.ToString(dr2["Tipo"]);
                                oBECelular.Stock = Convert.ToInt32(dr2["Cantidad"]);
                                oBECelular.Precio = Convert.ToDouble(dr2["Precio"]);

                                listaProductos.Add(oBECelular);
                            }
                        }
                    }
                    oBEVenta.Productos = listaProductos;
                    listaVentasCliente.Add(oBEVenta);
                }
                return listaVentasCliente;
            }
            else
            {
                return null;
            }
        }

    }
}
