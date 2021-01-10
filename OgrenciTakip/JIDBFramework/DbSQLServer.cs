using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JIDBFramework
{
    public class DbSQLServer
    {
        //Yurutucu, yurutme sorgusu
        private string _connstring;

        public DbSQLServer(string connstring)
        {
            _connstring = connstring;
        }

        
        //Dizi, Veri Tablosu, Veri Kumesi

        public DataTable GetDataList(string storeProceName)
        {
            DataTable dtData = new DataTable();
            using (SqlConnection conn = new SqlConnection(_connstring))
            {
                using (SqlCommand cmd = new SqlCommand(storeProceName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    dtData.Load(reader);
                }
            }
                return dtData;

        }

        public DataTable GetDataList(string storeProceName, DbParameter parameter)
        {
            DataTable dtData = new DataTable();
            using (SqlConnection conn = new SqlConnection(_connstring))
            {
                using (SqlCommand cmd = new SqlCommand(storeProceName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    cmd.Parameters.AddWithValue(parameter.Parameter, parameter.Value);

                    SqlDataReader reader = cmd.ExecuteReader();

                    dtData.Load(reader);
                }
            }
            return dtData;

        }

        public DataTable GetDataList(string storeProceName, DbParameter[] parameters)
        {
            DataTable dtData = new DataTable();
            using (SqlConnection conn = new SqlConnection(_connstring))
            {
                using (SqlCommand cmd = new SqlCommand(storeProceName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    foreach (var para in parameters)
                    {
                        cmd.Parameters.AddWithValue(para.Parameter, para.Value);
                    }
                    SqlDataReader reader = cmd.ExecuteReader();

                    dtData.Load(reader);
                }
            }
            return dtData;

        }

        public void SaveOrUpdateRecord(string storedProceName, object obj)
        {
            using (SqlConnection conn = new SqlConnection(_connstring))
            {
                using(SqlCommand cmd = new SqlCommand(storedProceName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    Type type = obj.GetType();
                    BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
                    PropertyInfo[] properties = type.GetProperties(flags);

                    foreach(var property in properties )
                    {
                        cmd.Parameters.AddWithValue("@" + property.Name, property.GetValue(obj, null));
                    }


                    cmd.ExecuteNonQuery();
                }
            }
        }


        //Overload
        public object GetScalarValue(string storedProceName)
        {
            object value = null;

            using (SqlConnection conn = new SqlConnection(_connstring))
            {
                using (SqlCommand cmd = new SqlCommand(storedProceName, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    conn.Open();

                    value = cmd.ExecuteScalar();
                }
            }
            return value;
        }



        public object GetScalarValue(string storedProceName, DbParameter parameter)
        {
            object value = null;

            using (SqlConnection conn = new SqlConnection(_connstring))
            {
                using (SqlCommand cmd = new SqlCommand(storedProceName, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    conn.Open();

                    cmd.Parameters.AddWithValue(parameter.Parameter, parameter.Value);

                    value = cmd.ExecuteScalar();
                }
                
            }
            return value;

        }

        public object GetScalarValue(string storedProceName, DbParameter[] parameters)
        {
            object value = null;

            using (SqlConnection conn = new SqlConnection(_connstring))
            {
                using (SqlCommand cmd = new SqlCommand(storedProceName, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    conn.Open();

                    foreach(var para in parameters)
                    {
                        cmd.Parameters.AddWithValue(para.Parameter, para.Value);
                    }

                   
                    value = cmd.ExecuteScalar();
                }

            }
            return value;

        }
    }
}



