using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstraccion;
using BE;
using DAL;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace MPP
{
    public class MPPVenta : IGestor<BEVenta>
    {
        public MPPVenta() { oData = new Data(); }


        Data oData;
        ArrayList AL;


        public bool Borrar(BEVenta obj)
        {
            throw new NotImplementedException();
        }

        public bool Guardar(BEVenta oBEVenta)
        {
            string Consulta = "s_Venta_Alta";
            AL = new ArrayList();

            SqlParameter sqlparam1 = new SqlParameter("@CodEmpleado", oBEVenta.Empleado.Codigo);
            sqlparam1.SqlDbType = SqlDbType.Int;
            AL.Add(sqlparam1);

            SqlParameter sqlparam2 = new SqlParameter("@CodCliente", oBEVenta.Cliente.Codigo);
            sqlparam2.SqlDbType = SqlDbType.Int;
            AL.Add(sqlparam2);

            SqlParameter sqlparam3 = new SqlParameter("@Fecha", oBEVenta.Fecha);
            sqlparam3.SqlDbType = SqlDbType.DateTime;
            AL.Add(sqlparam3);

            SqlParameter sqlparam4 = new SqlParameter("@Total", oBEVenta.Total);
            sqlparam4.SqlDbType = SqlDbType.Money;
            AL.Add(sqlparam4);

            SqlParameter sqlparam5 = new SqlParameter("@Descuento", oBEVenta.Descuento);
            sqlparam5.SqlDbType = SqlDbType.Money;
            AL.Add(sqlparam5);

            return oData.Escribir(Consulta, AL);
        }

        public bool RegistrarDetalle_Venta(BEVenta oBEVenta)
        {
            if (oBEVenta.Productos.Count > 0)
            {
                AL = new ArrayList();
                string Consulta = "s_Detalle_Venta_Alta";
                foreach (BEProducto oBEProducto in oBEVenta.Productos)
                {
                    AL.Clear();
                    SqlParameter sqlparam1 = new SqlParameter("@CodVenta", oBEVenta.Codigo);
                    sqlparam1.SqlDbType = SqlDbType.Int;
                    AL.Add(sqlparam1);

                    SqlParameter sqlparam2 = new SqlParameter("@CodProducto", oBEProducto.Codigo);
                    sqlparam1.SqlDbType = SqlDbType.Int;
                    AL.Add(sqlparam2);

                    SqlParameter sqlparam3 = new SqlParameter("@Cantidad", oBEProducto.Stock);
                    sqlparam1.SqlDbType = SqlDbType.Int;
                    AL.Add(sqlparam3);

                    SqlParameter sqlparam4 = new SqlParameter("@Precio", oBEProducto.Precio);
                    sqlparam1.SqlDbType = SqlDbType.Money;
                    AL.Add(sqlparam4);

                    oData.Escribir(Consulta, AL);

                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<BEVenta> ListarTodo()
        {
            List<BEVenta> listaVentas = new List<BEVenta>();
            AL = new ArrayList();
            DataTable dTable = new DataTable();
            string Consulta = "s_Venta_ListarT";

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
                    BECliente oBECliente = new BECliente();
                    oBECliente.Codigo = Convert.ToInt32(dr["CodCliente"]);
                    oBECliente.Nombre = Convert.ToString(dr["NCliente"]);
                    oBECliente.Apellido = Convert.ToString(dr["ACliente"]);
                    oBECliente.DNI = Convert.ToString(dr["DniCliente"]);
                    oBECliente.Telefono = Convert.ToString(dr["Telefono"]);
                    oBECliente.Email = Convert.ToString(dr["Email"]);
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
                            if (dr2["Tipo"].ToString() == "Computadora")
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
                    listaVentas.Add(oBEVenta);
                }
                return listaVentas;
            }
            else
            {
                return null;
            }


        }


        public BEVenta ListarVentaMayor()
        {
            string Consulta = "s_Venta_MTotal";
            DataTable dTable = new DataTable();
            dTable = oData.Leer(Consulta, null);
            if (dTable.Rows.Count > 0)
            {
                BEVenta oBEVenta = new BEVenta();
                foreach (DataRow dr in dTable.Rows)
                {
                    oBEVenta.Codigo = Convert.ToInt32(dr["CodVenta"]);
                    BEEmpleado oBEEmpleado = new BEEmpleado();
                    oBEEmpleado.Codigo = Convert.ToInt32(dr["CodEmpleado"]);
                    oBEEmpleado.Nombre = Convert.ToString(dr["NEmpleado"]);
                    oBEEmpleado.Apellido = Convert.ToString(dr["AEmpleado"]);
                    oBEEmpleado.Dni = Convert.ToString(dr["DniEmpleado"]);
                    oBEEmpleado.FechaIngreso = Convert.ToDateTime(dr["FechaIngreso"]);
                    oBEVenta.Empleado = oBEEmpleado;
                    BECliente oBECliente = new BECliente();
                    oBECliente.Codigo = Convert.ToInt32(dr["CodCliente"]);
                    oBECliente.Nombre = Convert.ToString(dr["NCliente"]);
                    oBECliente.Apellido = Convert.ToString(dr["ACliente"]);
                    oBECliente.DNI = Convert.ToString(dr["DniCliente"]);
                    oBECliente.Telefono = Convert.ToString(dr["Telefono"]);
                    oBECliente.Email = Convert.ToString(dr["Email"]);
                    oBEVenta.Cliente = oBECliente;
                    oBEVenta.Fecha = Convert.ToDateTime(dr["Fecha"]);
                    oBEVenta.Total = Convert.ToDouble(dr["Total"]);
                    oBEVenta.Descuento = Convert.ToDouble(dr["Descuento"]);
                }
                return oBEVenta;
            }
            else { return null; }
        }

    }
}
