using BoiMela.Dtos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;

namespace BoiMela.DataAccess
{
    public class SummaryStatDataAccess
    {
        public static SummaryStatDtos GetSummaryStatDataAccess() 
        {
            SummaryStatDtos stats = new SummaryStatDtos();

            using(SqlConnection conn = Connection.GetSqlConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "dbo.spGetSummaryStats",
                    CommandTimeout = 0,
                };
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    stats.TotalBooks = (Int32)reader["TotalBooks"];
                    stats.TotalGenres = (Int32)reader["TotalGenres"];
                    stats.TotalWriters = (Int32)reader["TotalWriters"];
                    stats.TotalPublications = (Int32)reader["TotalPublications"];
                    stats.TotalSales = (Int32)reader["TotalSales"];
                    stats.TotalCustomers = (Int32)reader["TotalCustomers"];
                    stats.SoldBooks = (Int32)reader["SoldBooks"];
                    stats.InStockBooks = (Int32)reader["InStockBooks"];
                    stats.TotalCustomers = (Int32)reader["TotalCustomers"];

                }

            }
            return stats;
        }
    }
}