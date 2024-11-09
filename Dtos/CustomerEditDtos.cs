using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoiMela.Dtos
{
    public class CustomerEditDtos
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}