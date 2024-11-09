using BoiMela.Models;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;

namespace BoiMela.DataAccess
{
    public class BookDataAccess
    {
        public static List<Book> GetBooksData()
        {
            List<Book> list = new List<Book>();
            using (SqlConnection connection = Connection.GetSqlConnection())
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spBGgetProductList"
                };
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 0;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DateTime dateTime = DateTime.Parse(reader["create_date"].ToString());
                    Book book = new Book {
                        Id = Convert.ToInt32(reader["id"].ToString()),
                        Title = reader["title"].ToString(),
                        Price = Convert.ToDecimal(reader["price"].ToString()),
                        Publication = reader["publication"].ToString(),
                        Writer = reader["writer"].ToString(),
                        Genre = reader["genere"].ToString(),
                        Quantity = Convert.ToInt32(reader["quantity"].ToString()),
                        Stock = Convert.ToInt32(reader["stock"].ToString()),
                        UploadDate = dateTime.ToString("d/M/yyyy"),

                    };
                    list.Add(book);
                }
            }

            return list;

        }
        public static Book GetBookById(int id)
        {
            Book book = new Book();
            using (SqlConnection connection = Connection.GetSqlConnection())
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spBGgetProductById"
                };
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@id", id));
                cmd.CommandTimeout = 0;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DateTime dateTime = DateTime.Parse(reader["create_date"].ToString());
                    book.Id = Convert.ToInt32(reader["id"].ToString());
                    book.Title = reader["title"].ToString();
                    book.Price = Convert.ToDecimal(reader["price"].ToString());
                    book.Publication = reader["publication"].ToString();
                    book.Writer = reader["writer"].ToString();
                    book.Genre = reader["genere"].ToString();
                    book.Description = reader["description"].ToString();
                    book.Quantity = Convert.ToInt32(reader["quantity"].ToString());
                    book.Stock = Convert.ToInt32(reader["stock"].ToString());
                    book.UploadDate = dateTime.ToString("d/M/yyyy");

                };
            }
            return book;
        }
        public static int InsertBook(string title, string writer, string publication, string genre, string description, decimal price, int qunt)
        {
            int result;
            using (SqlConnection conn = Connection.GetSqlConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spGBInsertBook"
                };
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter ("@title", title));
                cmd.Parameters.Add(new SqlParameter("@writer", writer));
                cmd.Parameters.Add(new SqlParameter("@publication", publication));
                cmd.Parameters.Add(new SqlParameter("@genre", genre));
                cmd.Parameters.Add(new SqlParameter("@description", description));
                cmd.Parameters.Add(new SqlParameter("@price", price));
                cmd.Parameters.Add(new SqlParameter("@qunt", qunt));

                cmd.CommandTimeout = 0;

                result = cmd.ExecuteNonQuery();
            }
            return result;
        }
        public static int EditBook(int id, string title, string writer, string publication, string genre, string description, decimal price, int qunt)
        {
            int result;
            using (SqlConnection conn = Connection.GetSqlConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spGBUpdBook"
                };
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@id", id));
                cmd.Parameters.Add(new SqlParameter("@title", title));
                cmd.Parameters.Add(new SqlParameter("@writer", writer));    
                cmd.Parameters.Add(new SqlParameter("@publication", publication));
                cmd.Parameters.Add(new SqlParameter("@genre", genre));
                cmd.Parameters.Add(new SqlParameter("@description", description));
                cmd.Parameters.Add(new SqlParameter("@price", price));
                cmd.Parameters.Add(new SqlParameter("@qunt", qunt));

                cmd.CommandTimeout = 0;

                result = cmd.ExecuteNonQuery();
            }
            return result;
        }
        public static int DeleteBook(int id)
        {
            int result;
            using (SqlConnection conn = Connection.GetSqlConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spGBDeleteBook"
                };
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@id", id));

                cmd.CommandTimeout = 0;

                result = cmd.ExecuteNonQuery();
            }
            return result;
        }
    }
}