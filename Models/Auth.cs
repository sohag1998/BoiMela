﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoiMela.Models
{
    public class Auth
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }
}