using BoiMela.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace BoiMela.DataAccess
{
    public class PublicationDataAccess
    {
        public static List<Publication> GetPublicationList()
        {
            List<Publication> list = new List<Publication>();

            using (SqlConnection conn = Connection.GetSqlConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spGBShowPublicationList",
                    CommandTimeout = 0
                };
                cmd.Parameters.Clear();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Publication publication = new Publication
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Name = Convert.ToString(reader["name"]),
                    };

                    list.Add(publication);
                }
            }

            return list;
        }
        public static Publication GetPublicationById(int id)
        {
            Publication publication = new Publication();

            using (SqlConnection conn = Connection.GetSqlConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spGBShowPublicationById",
                    CommandTimeout = 0
                };
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@id", id));

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    publication.Id = Convert.ToInt32(reader["id"]);
                    publication.Name = Convert.ToString(reader["name"]);
                }
            }

            return publication;
        }
        public static int InsertPublication(string publicationName)
        {
            int result = 0;
            using (SqlConnection conn = Connection.GetSqlConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spGBInsPublication",
                    CommandTimeout = 0
                };
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@name", publicationName));
                result = cmd.ExecuteNonQuery();
            }

            return result;
        }
        public static int EditPublication(int id, string publicationName)
        {
            int result = 0;
            using (SqlConnection conn = Connection.GetSqlConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spGBUpdPublication",
                    CommandTimeout = 0
                };
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@id", id));
                cmd.Parameters.Add(new SqlParameter("@name", publicationName));
                result = cmd.ExecuteNonQuery();
            }

            return result;
        }
        public static int DeletePublication(int id)
        {
            int result = 0;
            using (SqlConnection conn = Connection.GetSqlConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spGBDeletePublication",
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