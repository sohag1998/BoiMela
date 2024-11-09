using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using BoiMela.Models;
using System.Web.UI.WebControls;

namespace BoiMela.DataAccess
{
    public class SalesDataAccess
    {
        public static DataTable GetSaleList()
        {
            DataTable dt = new DataTable();
            using(SqlConnection conn = Connection.GetSqlConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spGBgetOrdersList"
                };
                cmd.Parameters.Clear();
                cmd.CommandTimeout = 0;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);

            }
            return dt;
        }
        public static SalesDetalis GetSaleDetails(int orderId)
        {
            SalesDetalis salesDetalis = new SalesDetalis();
            using (SqlConnection conn = Connection.GetSqlConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.GBgetOrderDetalis"
                };
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@orderId", orderId));
                cmd.CommandTimeout = 0;
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    salesDetalis.OrderId = Convert.ToInt32(reader["order_id"]);
                    salesDetalis.CustName = reader["custName"].ToString();
                    salesDetalis.Status = reader["status"].ToString();
                    salesDetalis.Quantity = Convert.ToInt32(reader["total_book_quantity"]);
                    salesDetalis.Email = reader["email"].ToString();
                    salesDetalis.Phone = reader["phone"].ToString();
                    salesDetalis.Address = reader["address"].ToString();

                }

            }
            return salesDetalis;
        }
        public static List<SoldBook> GetSalesBookList(int order_id) 
        { 
            List<SoldBook> list = new List<SoldBook>();
            using(SqlConnection conn = Connection.GetSqlConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spGBgetOrderBookList"
                };
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@order_id", order_id));
                cmd.CommandTimeout = 0;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SoldBook soldBook = new SoldBook
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        OrderId = Convert.ToInt32(reader["order_id"]),
                        BookId = Convert.ToInt32(reader["book_id"]),
                        Name = reader["title"].ToString(),
                        Quantity = Convert.ToInt32(reader["quantity"]),
                        Price = Convert.ToDecimal(reader["price"])
                    };
                    list.Add(soldBook);

                }
            }
            return list;
            
        }
        public static int InsertSale(Dictionary<int, int> bookQuantities, int custId)
        {
            int result = 0;

            using (SqlConnection conn = Connection.GetSqlConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spGBInsertOrder"
                };
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@cust_id", custId));
                SqlParameter outputIdParam = new SqlParameter("@latest_id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputIdParam);

                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();

                int latestId = (int)outputIdParam.Value;

                result = InsertOrderBook(latestId, bookQuantities);


            }

            return result;
        }
        public static int InsertOrderBook(int order_id, Dictionary<int, int> bookQuantities)
        {
            int result = 0;
            foreach(var book in bookQuantities)
            {
                int bookId = book.Key;
                int quantity = book.Value;
                using (SqlConnection conn = Connection.GetSqlConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = conn,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "dbo.spGBinsertOrderBook"
                    };
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@order_id", order_id));
                    cmd.Parameters.Add(new SqlParameter("@book_id", Convert.ToInt32(bookId)));
                    cmd.Parameters.Add(new SqlParameter("@quantity", Convert.ToInt32(quantity)));
                    cmd.CommandTimeout = 0;
                    result = cmd.ExecuteNonQuery();
                }

            }
            return result;
        }
        public static int UpdateOrder(int orderId, string orderStatus, Dictionary<int, int> bookQuantites)
        {
            int result = 0;
            using (SqlConnection conn = Connection.GetSqlConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spGBupdateOrder"

                };
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@id", orderId));
                cmd.Parameters.Add(new SqlParameter("@status", orderStatus));

                cmd.CommandTimeout = 0;
                result = cmd.ExecuteNonQuery();
                if(result == 1)
                {
                    result = UpdateOrderBook(orderId, bookQuantites);
                }
            }
            return result;
        }

        public static int UpdateOrderBook(int orderId, Dictionary<int, int> bookQty)
        {
            int result = 0;
            foreach(var book in bookQty)
            {
                int bookId = book.Key;
                int qty = book.Value;


                using (SqlConnection conn = Connection.GetSqlConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = conn,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "dbo.spGBupdateOrderBook"
                    };

                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@orderId", orderId));
                    cmd.Parameters.Add(new SqlParameter("@bookId", bookId));
                    cmd.Parameters.Add(new SqlParameter("@quantity", qty));
                    cmd.CommandTimeout = 0;

                    result = cmd.ExecuteNonQuery();

                }
            }
            return result;
        }
    }
}