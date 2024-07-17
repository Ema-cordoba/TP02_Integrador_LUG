using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using Abstraccion;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace MPP
{
    public class MPPUsuario : IGestor<BEUsuario>
    {
        public MPPUsuario() { oData = new Data(); }

        Data oData;
        ArrayList AL;

        public bool Borrar(BEUsuario oBEUsuario)
        {
            AL = new ArrayList();
            string Consulta = "s_Usuario_Baja";
            SqlParameter sqlparam = new SqlParameter("@CodEmpleado", oBEUsuario.Empleado.Codigo);
            sqlparam.SqlDbType = SqlDbType.Int;
            AL.Add(sqlparam);
            return oData.Escribir(Consulta, AL);
        }


        public BEUsuario VerificarUsuario(string User, string Password)
        {
            oData = new Data();
            if (oData.Login(User, Password) == 1)
            {
                AL = new ArrayList();
                string Consulta = "s_Usuario_Listar";
                SqlParameter sqlparam1 = new SqlParameter("@User", User);
                sqlparam1.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlparam1);

                SqlParameter sqlparam2 = new SqlParameter("@Password", Password);
                sqlparam1.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlparam2);

                DataTable dTable = oData.Leer(Consulta, AL);
                if (dTable.Rows.Count > 0)
                {
                    BEUsuario oBEUsuario = new BEUsuario();
                    foreach (DataRow dr in dTable.Rows)
                    {
                        oBEUsuario.Codigo = Convert.ToInt32(dr["CodUsuario"]);
                        oBEUsuario.NombreUsuario = Convert.ToString(dr["Usuario"]);
                        oBEUsuario.Clave = Convert.ToString(dr["Clave"]);
                        oBEUsuario.Administrador = Convert.ToBoolean(dr["Administrador"]);
                        BEEmpleado oBEEmpleado = new BEEmpleado();
                        oBEEmpleado.Codigo = Convert.ToInt32(dr["CodEmpleado"]);
                        oBEEmpleado.Nombre = Convert.ToString(dr["Nombre"]);
                        oBEEmpleado.Apellido = Convert.ToString(dr["Apellido"]);
                        oBEEmpleado.Dni = Convert.ToString(dr["DNI"]);
                        oBEEmpleado.FechaIngreso = Convert.ToDateTime(dr["FechaIngreso"]);
                        oBEUsuario.Empleado = oBEEmpleado;
                    }
                    return oBEUsuario;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public bool VerificarNombreUser(BEUsuario oBEUsuario)
        {
            AL = new ArrayList();
            string Consulta = "SELECT COUNT(*) USUARIO FROM USUARIO WHERE USUARIO.Usuario = '" + oBEUsuario.NombreUsuario + "'";
            return oData.LeerScalar(Consulta);
        }

        public bool Guardar(BEUsuario oBEUsuario)
        {
            AL = new ArrayList();
            string Consulta = string.Empty;
            if (oBEUsuario.Codigo == 0)
            {
                Consulta = "s_Usuario_Alta";

                SqlParameter sqlparam1 = new SqlParameter("@NombreUsuario", oBEUsuario.NombreUsuario);
                sqlparam1.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlparam1);

                SqlParameter sqlparam2 = new SqlParameter("@Clave", oBEUsuario.Clave);
                sqlparam2.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlparam2);

                SqlParameter sqlparam3 = new SqlParameter("@CodEmpleado", oBEUsuario.Empleado.Codigo);
                sqlparam3.SqlDbType = SqlDbType.Int;
                AL.Add(sqlparam3);

                SqlParameter sqlparam4 = new SqlParameter("@Admin", oBEUsuario.Administrador);
                sqlparam4.SqlDbType = SqlDbType.Bit;
                AL.Add(sqlparam4);

                return oData.Escribir(Consulta, AL);
            }
            else
            {
                Consulta = "s_Usuario_Modificar";

                SqlParameter sqlparam1 = new SqlParameter("@Codigo", oBEUsuario.Codigo);
                sqlparam1.SqlDbType = SqlDbType.Int;
                AL.Add(sqlparam1);

                SqlParameter sqlparam2 = new SqlParameter("@NombreUsuario", oBEUsuario.NombreUsuario);
                sqlparam2.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlparam2);

                SqlParameter sqlparam3 = new SqlParameter("@Clave", oBEUsuario.Clave);
                sqlparam3.SqlDbType = SqlDbType.NVarChar;
                AL.Add(sqlparam3);

                SqlParameter sqlparam4 = new SqlParameter("@Admin", oBEUsuario.Administrador);
                sqlparam4.SqlDbType = SqlDbType.Bit;
                AL.Add(sqlparam4);

                return oData.Escribir(Consulta, AL);
            }
        }

        public List<BEUsuario> ListarTodo()
        {
            throw new NotImplementedException();
        }
    }
}
