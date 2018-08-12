using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _25948083_Wassenaar_L_Project_3.Models
{
    public class Login
    {
        string user_name;
        string password;

        public Login(string user_name, string password)
        {
            this.user_name = user_name;
            this.password = password;
        }

        public string getUserName()
        {
            return user_name;
        }

        public string getPassword()
        {
            return password;
        }
    }
}