using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace _25948083_Wassenaar_L_Project_3.Models
{
    public class UserModel
    {
        public string user_id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password Required")]
        [DisplayName("Password")]
        [StringLength(55, ErrorMessage = ":Less than 55 characters")]
        public string password { get; set; }
    }
}