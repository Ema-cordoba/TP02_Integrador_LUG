using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Abstraccion;
using BE;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace MPP
{
    public class MPPEmpleado : IGestor<BEEmpleado>
    {

        public MPPEmpleado() { oData = new Data(); }

        Data oData;
        ArrayList AL;

        public bool Borrar(BEEmpleado oBEEmpleado)
        {
            AL = new ArrayList();
            string Consulta = "s_Empleado_Baja";
            SqlParameter sqlparam = new SqlParameter("@Codigo", oBEEmpleado.Codigo);
            sqlparam.SqlDbType = SqlDbType.Int;
            AL.Add(sqlparam);

            return oData.Escribir(Consulta, AL);
        }



        public bool obtenerTotalVentas(BEEmpleado oBEEmpleado)
        {
            // consulta para obtener el total de las ventas de un empleado para un reporte
            string Consulta = "SELECT COUNT(*) FROM VENTA WHERE CodEmpleado = '" + oBEEmpleado.Codigo + "'";
            return oData.LeerScalar(Consulta);
        }

        public bool Guardar(BEEmpleado oBEEmpleado)
        {

            AL = new ArrayList();
            string Consulta = string.Empty;
            if (oBEEmpleado.Codigo == 0)
            {
                Consulta = "s_Empleado_Alta";

                SqlParameter sqlparam1 = new SqlParameter("@Nombre", oBEEmpleado.Nombre);
                sqlparam1.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlparam1);

                SqlParameter sqlparam2 = new SqlParameter("@Apellido", oBEEmpleado.Apellido);
                sqlparam2.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlparam2);

                SqlParameter sqlparam3 = new SqlParameter("@Dni", oBEEmpleado.Dni);
                sqlparam3.SqlDbType = SqlDbType.Char;
                AL.Add(sqlparam3);

                SqlParameter sqlparam4 = new SqlParameter("@FechaIngreso", oBEEmpleado.FechaIngreso);
                sqlparam4.SqlDbType = SqlDbType.Date;
                AL.Add(sqlparam4);

                return oData.Escribir(Consulta, AL);
            }
            else
            {
                Consulta = "s_Empleado_Modificar";

                SqlParameter sqlparam1 = new SqlParameter("@Codigo", oBEEmpleado.Codigo);
                sqlparam1.SqlDbType = SqlDbType.Int;
                AL.Add(sqlparam1);

                SqlParameter sqlparam2 = new SqlParameter("@Nombre", oBEEmpleado.Nombre);
                sqlparam2.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlparam2);

                SqlParameter sqlparam3 = new SqlParameter("@Apellido", oBEEmpleado.Apellido);
                sqlparam3.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlparam3);

                SqlParameter sqlparam4 = new SqlParameter("@Dni", oBEEmpleado.Dni);
                sqlparam4.SqlDbType = SqlDbType.Char;
                AL.Add(sqlparam4);

                SqlParameter sqlparam5 = new SqlParameter("@FechaIngreso", oBEEmpleado.FechaIngreso);
                sqlparam5.SqlDbType = SqlDbType.Date;
                AL.Add(sqlparam5);

                return oData.Escribir(Consulta, AL);
            }
        }

        public List<BEEmpleado> ListarTodo()
        {
            List<BEEmpleado> listaEmpleados = new List<BEEmpleado>();
            AL = new ArrayList();

            string Consulta = "s_Empleado_ListarT";
            DataTable dTable = new DataTable();
            dTable = oData.Leer(Consulta, null);

            if (dTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dTable.Rows)
                {
                    BEEmpleado oBEEmpleado = new BEEmpleado();
                    oBEEmpleado.Codigo = Convert.ToInt32(dr["Codigo"]);
                    oBEEmpleado.Nombre = Convert.ToString(dr["Nombre"]);
                    oBEEmpleado.Apellido = Convert.ToString(dr["Apellido"]);
                    oBEEmpleado.Dni = Convert.ToString(dr["DNI"]);
                    oBEEmpleado.FechaIngreso = Convert.ToDateTime(dr["FechaIngreso"]);

                    listaEmpleados.Add(oBEEmpleado);
                }
                return listaEmpleados;
            }
            else
            {
                return null;
            }
        }


        public List<BEVenta> ListarVentasEmpleado(BEEmpleado oBEEmpleado)
        {
            List<BEVenta> listaVentasEmpleado = new List<BEVenta>();
            AL = new ArrayList();
            DataTable dTable = new DataTable();
            string Consulta = "s_Empleado_ListarV";

            SqlParameter sqlparam1 = new SqlParameter("@CodEmpleado", oBEEmpleado.Codigo);
            sqlparam1.SqlDbType = SqlDbType.Int;
            AL.Add(sqlparam1);

            dTable = oData.Leer(Consulta, AL);

            if (dTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dTable.Rows)
                {
                    BEVenta oBEVenta = new BEVenta();
                    oBEVenta.Codigo = Convert.ToInt32(dr["CodVenta"]);
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
                    listaVentasEmpleado.Add(oBEVenta);
                }
                return listaVentasEmpleado;
            }
            else
            {
                return null;
            }
        }
        public List<BEEmpleado> listarEmpleadosSinUsuario()
        {
            // lista de empleados sin usuarios para el formulario de adminitrador encargado de
            // registrar los empleados en el sistema
            List<BEEmpleado> listaEmpleados = new List<BEEmpleado>();
            string Consulta = "s_Empleado_SinUser";
            DataTable dTable = new DataTable();
            dTable = oData.Leer(Consulta, null);

            if (dTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dTable.Rows)
                {
                    BEEmpleado oBEEmpleado = new BEEmpleado();
                    oBEEmpleado.Codigo = Convert.ToInt32(dr["Codigo"]);
                    oBEEmpleado.Nombre = Convert.ToString(dr["Nombre"]);
                    oBEEmpleado.Apellido = Convert.ToString(dr["Apellido"]);
                    oBEEmpleado.Dni = Convert.ToString(dr["DNI"]);
                    oBEEmpleado.FechaIngreso = Convert.ToDateTime(dr["FechaIngreso"]);

                    listaEmpleados.Add(oBEEmpleado);
                }
                return listaEmpleados;
            }
            else
            {
                return null;
            }
        }


        public BEEmpleado ListarEmpleadoMasVentas()
        {
            // retorna el empleado con más ventas realizadas
            string Consulta = "s_Empleado_MasV";
            DataTable dTable = new DataTable();
            dTable = oData.Leer(Consulta, null);
            if (dTable.Rows.Count > 0)
            {
                BEEmpleado oBEEmpleado = new BEEmpleado();
                foreach (DataRow dr in dTable.Rows)
                {
                    oBEEmpleado.Codigo = Convert.ToInt32(dr["Codigo"]);
                    oBEEmpleado.Nombre = Convert.ToString(dr["Nombre"]);
                    oBEEmpleado.Apellido = Convert.ToString(dr["Apellido"]);
                    oBEEmpleado.Dni = Convert.ToString(dr["DNI"]);
                    oBEEmpleado.FechaIngreso = Convert.ToDateTime(dr["FechaIngreso"]);
                }
                return oBEEmpleado;
            }
            else
            {
                return null;
            }
        }







    }
}
