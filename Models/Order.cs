using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoiMela.Controllers
{
    public class Order
    {
       public int OrderId { get; set; }
       public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}