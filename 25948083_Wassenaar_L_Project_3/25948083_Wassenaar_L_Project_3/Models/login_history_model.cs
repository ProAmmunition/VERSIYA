using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _25948083_Wassenaar_L_Project_3.Models
{
    public class login_history_model
    {
        // used for user history with display annotations
        [Display(Name = "Login Number")]
        public string login_id { get; set; }

        [Display(Name = "Username")]
        public string username { get; set; }

        [Display(Name = "Date")]
        public string login_date { get; set; }
    }
}