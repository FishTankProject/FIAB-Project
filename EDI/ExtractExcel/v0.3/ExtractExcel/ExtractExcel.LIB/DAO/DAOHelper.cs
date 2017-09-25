using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractExcel.Lib.DAO
{
    public class DAOHelper
    {
        public static void InsertData(SqlCommand command)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                command.Connection = connection;
                connection.Open();
                try
                {
                    int rowAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("ERROR : " + ex.Message);
                }
                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }

        public static int RetreiveID(SqlCommand command)
        {
            int id = -1;

            using (SqlConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                command.Connection = connection;
                connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    if(reader.HasRows)
                    {
                        reader.Read();
                        id = (int)reader[0];
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("ERROR : " + ex.Message);
                }
                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }

            return id;
        }

        public static string RetreiveString(SqlCommand command)
        {
            string result = string.Empty;

            using (SqlConnection connection = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                command.Connection = connection;
                connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        result = (string)reader[0];
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("ERROR : " + ex.Message);
                }
                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }

            return result;
        }
    }
}
