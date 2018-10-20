using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace _25948083_Wassenaar_L_Project_3.Models
{
    //used for user-loggin with required, stringlength, display and datatype annotations
    public class LoginModel
    {
        [Required(ErrorMessage ="Please enter username")]
        [StringLength(25, ErrorMessage = "Invalid username length", MinimumLength = 3)]
        [Display(Name ="Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [StringLength(55, ErrorMessage = "Invalid password length", MinimumLength = 6)]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}