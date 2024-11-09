using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoiMela.Dtos
{
    public class SummaryStatDtos
    {
        public int TotalBooks { get; set; }
        public int TotalGenres { get; set; }
        public int TotalPublications { get; set; }
        public int TotalWriters { get; set; }
        public int TotalSales { get; set; }
        public int SoldBooks { get; set; }
        public int InStockBooks { get; set; }
        public int TotalCustomers { get; set; }

    }
}
