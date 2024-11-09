using BoiMela.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace BoiMela.DataAccess
{
    public class GenreDataAccess
    {
        public static List<Genre> GetGenreList()
        {
            List<Genre> list = new List<Genre>();

            using (SqlConnection conn = Connection.GetSqlConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spGBShowGenreList",
                    CommandTimeout = 0
                };
                cmd.Parameters.Clear();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Genre genre = new Genre
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Name = Convert.ToString(reader["name"]),
                    };

                    list.Add(genre);
                }
            }

            return list;
        }
        public static Genre GetGenreById(int id)
        {
            Genre genre = new Genre();

            using (SqlConnection conn = Connection.GetSqlConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spGBShowGenreById",
                    CommandTimeout = 0
                };
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@id", id));

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    genre.Id = Convert.ToInt32(reader["id"]);
                    genre.Name = Convert.ToString(reader["name"]);
                }
            }

            return genre;
        }
        public static int InsertGenre(string genreName)
        {
            int result = 0;
            using (SqlConnection conn = Connection.GetSqlConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spGBInsGenre",
                    CommandTimeout = 0
                };
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@name", genreName));
                result = cmd.ExecuteNonQuery();
            }

            return result;
        }
        public static int EditGenre(int id, string genreName)
        {
            int result = 0;
            using (SqlConnection conn = Connection.GetSqlConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spGBUpdGenre",
                    CommandTimeout = 0
                };
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@id", id));
                cmd.Parameters.Add(new SqlParameter("@name", genreName));
                result = cmd.ExecuteNonQuery();
            }

            return result;
        }
        public static int DeleteGenre(int id)
        {
            int result = 0;
            using (SqlConnection conn = Connection.GetSqlConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spGBDeleteGenre",
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