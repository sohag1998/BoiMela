using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoiMela.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Publication { get; set; }
        public string Writer { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Stock {  get; set; }
        public string UploadDate { get; set; }
    }
}