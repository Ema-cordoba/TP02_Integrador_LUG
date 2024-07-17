using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class Data
    {
        private SqlConnection oConection = new SqlConnection(@"Data Source=DESKTOP-FL6Q0Q5;Initial Catalog=TP02_LUG_Integrador_EmanuelCordoba;Integrated Security=True;Encrypt=False");
        private string stringConection = @"Data Source=DESKTOP-FL6Q0Q5;Initial Catalog=TP02_LUG_Integrador_EmanuelCordoba;Integrated Security=True;Encrypt=False";
        private SqlTransaction oTrans;
        private SqlCommand sqlCommand;

        public DataTable Leer(string consulta, ArrayList parametros)
        {
            SqlDataAdapter dAdapter;
            DataTable dTable = new DataTable();
            sqlCommand = new SqlCommand(consulta, oConection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            try
            {
                dAdapter = new SqlDataAdapter(sqlCommand);
                if (parametros != null)
                {
                    foreach (SqlParameter P in parametros)
                    {
                        sqlCommand.Parameters.AddWithValue(P.ParameterName, P.Value);
                    }
                }
            }
            catch (SqlException sqlex) { throw sqlex; }
            catch (Exception ex) { throw ex; }
            dAdapter.Fill(dTable);
            return dTable;
        }



        public bool Escribir(string consulta, ArrayList parametros)
        {
            if (oConection.State == ConnectionState.Closed)
            {
                oConection.ConnectionString = stringConection;
                oConection.Open();
            }

            try
            {
                oTrans = oConection.BeginTransaction();
                sqlCommand = new SqlCommand(consulta, oConection, oTrans);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                if (parametros != null)
                {
                    foreach (SqlParameter P in parametros)
                    {
                        sqlCommand.Parameters.AddWithValue(P.ParameterName, P.Value);
                    }
                }
                int Respuesta = sqlCommand.ExecuteNonQuery();
                oTrans.Commit();
                return true;
            }
            catch (SqlException sqlex) { oTrans.Rollback(); return false; }
            catch (Exception ex) { oTrans.Rollback(); return false; }
        }


        public bool LeerScalar(string consulta)
        {
            oConection.Open();
            SqlCommand sqlcmmd = new SqlCommand(consulta, oConection);
            sqlcmmd.CommandType = CommandType.Text;
            try
            {
                int respuesta = Convert.ToInt32(sqlcmmd.ExecuteScalar());
                if (respuesta > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException sqlex) { throw sqlex; }
            catch (Exception ex) { throw ex; }
            finally
            {
                oConection.Close();
            }
        }



        public int Login(string User, string Password)
        {
            try
            {
                oConection.Open();

                SqlCommand sqlcmmd = new SqlCommand("s_Usuario_Verificar", oConection);
                sqlcmmd.CommandType = CommandType.StoredProcedure;

                sqlcmmd.Parameters.AddWithValue("@Usuario", User);
                sqlcmmd.Parameters.AddWithValue("@Clave", Password);

                SqlDataReader dr = sqlcmmd.ExecuteReader();
                if (dr.Read())
                {
                    return dr.GetInt32(0);
                }
                else
                {
                    return 0;
                }
            }
            catch (SqlException sqlex) { throw sqlex; }
            catch (Exception ex) { throw ex; }
            finally
            {
                oConection.Close();
            }
        }



    }
}
