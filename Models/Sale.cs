using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoiMela.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public List<int> BookIdList { get;}
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
    }
    public class SoldBook
    { 
        public int Id { get; set; }
        public int OrderId {  get; set; }
        public int BookId { get; set; }
        public string Name { get; set; }
        public int Quantity {  get; set; }
        public decimal Price { get; set; }
        
    }
    public class SalesDetalis
    {
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public int CustomerId { get; set; }
        public string CustName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

}