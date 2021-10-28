using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class AuthenticateModel
    {
       
        public string Username { get; set; }

       
        public string Password { get; set; }
    }
}
