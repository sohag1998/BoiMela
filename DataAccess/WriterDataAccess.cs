using BoiMela.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BoiMela.DataAccess
{
    public class WriterDataAccess
    {
        public static List<Writer> GetWriterList()
        {
            List<Writer> list = new List<Writer>();

            using(SqlConnection conn = Connection.GetSqlConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spGBShowWriterList",
                    CommandTimeout = 0
                };
                cmd.Parameters.Clear();

                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    Writer writer = new Writer
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Name = Convert.ToString(reader["name"]),
                    };

                    list.Add(writer);
                }
            }

            return list;
        }
        public static Writer GetWriterById(int id)
        {
            Writer writer = new Writer();

            using (SqlConnection conn = Connection.GetSqlConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spGBShowWriterById",
                    CommandTimeout = 0
                };
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@id", id));

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    writer.Id = Convert.ToInt32(reader["id"]);
                    writer.Name = Convert.ToString(reader["name"]);
                }
            }

            return writer;
        }
        public static int InsertWriter(string writerName)
        {
            int result = 0;
            using(SqlConnection conn = Connection.GetSqlConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spGBInsWriter",
                    CommandTimeout = 0
                };
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@name", writerName));
                result = cmd.ExecuteNonQuery();
            }

            return result;
        }
        public static int EditWriter(int id, string writerName)
        {
            int result = 0;
            using (SqlConnection conn = Connection.GetSqlConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spGBUpdWriter",
                    CommandTimeout = 0
                };
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@id", id));
                cmd.Parameters.Add(new SqlParameter("@name", writerName));
                result = cmd.ExecuteNonQuery();
            }

            return result;
        }
        public static int DeleteWriter(int id)
        {
            int result = 0;
            using (SqlConnection conn = Connection.GetSqlConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spGBDeleteWriter",
                    CommandTimeout = 0
                };
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@id", id));
                result = cmd.ExecuteNonQuery();
            }

            return result;
        }
    }
}