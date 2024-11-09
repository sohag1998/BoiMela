using BoiMela.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using BoiMela.Dtos;

namespace BoiMela.DataAccess
{
    public class CustomerDataAccess
    {

        public static List<Customer> GetCustomerList()
        {
            var list = new List<Customer>();

            using (SqlConnection conn = Connection.GetSqlConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spGBgetCutomerList",
                    CommandTimeout = 0
                };
                cmd.Parameters.Clear();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Customer customer = new Customer
                    {
                        Id = Convert.ToInt32(reader["id"].ToString()),
                        Name = reader["fname"].ToString()+ " " + reader["lname"].ToString(),
                        Email = reader["email"].ToString(),
                        Phone = reader["phone"].ToString(),
                        Address = reader["address"].ToString()
                    };

                    list.Add(customer);
                }

            }

            return list;

        }
        public static CustomerEditDtos GetCustomerById(int id)
        {
            CustomerEditDtos customer = new CustomerEditDtos();

            using (SqlConnection conn = Connection.GetSqlConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spGBgetCutomerById",
                    CommandTimeout = 0
                };
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@id", id));

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    customer.Id = Convert.ToInt32(reader["id"].ToString());
                    customer.Fname = reader["fname"].ToString();
                    customer.Lname = reader["lname"].ToString();
                    customer.Email = reader["email"].ToString();
                    customer.Phone = reader["phone"].ToString();
                    customer.Address = reader["address"].ToString();
                }

            }

            return customer;

        }

        public static int InsertCustomer(string fname, string lname, string email, string phone, string address)
        {
            int result = 0;
            using(SqlConnection conn = Connection.GetSqlConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spGBInsCustomer",
                    CommandTimeout = 0
                };
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@fname", fname));
                cmd.Parameters.Add(new SqlParameter("@lname", lname));
                cmd.Parameters.Add(new SqlParameter("@email", email));
                cmd.Parameters.Add(new SqlParameter("@phone", phone));
                cmd.Parameters.Add(new SqlParameter("@address", address));

                result = cmd.ExecuteNonQuery();
            }

            return result;

        }
        public static int UpdateCustomer(int id, string fname, string lname, string email, string phone, string address)
        {
            int result = 0;
            using (SqlConnection conn = Connection.GetSqlConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spGBUpdCustomer",
                    CommandTimeout = 0
                };
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@id", id));
                cmd.Parameters.Add(new SqlParameter("@fname", fname));
                cmd.Parameters.Add(new SqlParameter("@lname", lname));
                cmd.Parameters.Add(new SqlParameter("@email", email));
                cmd.Parameters.Add(new SqlParameter("@phone", phone));
                cmd.Parameters.Add(new SqlParameter("@address", address));

                result = cmd.ExecuteNonQuery();
            }

            return result;

        }
        public static int DeleteCustomer(int id)
        {
            int result = 0;
            using (SqlConnection conn = Connection.GetSqlConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spGBDltCustomer",
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