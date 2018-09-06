﻿using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;





namespace _25948083_Wassenaar_L_Project_3.Models
{
    public class UserModel
    {
        [Display(Name = "USER ID")]
        [Required(ErrorMessage = "ID is required")]
        [StringLength(13, ErrorMessage = "Must be 13 characters", MinimumLength = 13)]
        public string User_id { get; set; }

        [Display(Name = "USERNAME")]
        [Required(ErrorMessage = "Username is required")]
        [StringLength(25, ErrorMessage = "Must be between 5 and 25 characters", MinimumLength = 5)]
        public string Username { get; set; }

        [Display(Name = "E-MAIL ADDRESS")]
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string Email { get; set; }

        [Display(Name = "PASSWORD")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password Required")]
        [StringLength(55, ErrorMessage = "Must be between 6 and 55 characters", MinimumLength = 6)]
        public string Password { get; set; }

        [Display(Name = "CONFIRM PASSWORD")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(55, ErrorMessage = "Must be between 6 and 55 characters", MinimumLength = 6)]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }
    }
}