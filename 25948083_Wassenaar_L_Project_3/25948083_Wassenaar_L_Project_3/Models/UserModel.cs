using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text;
using System.Security.Cryptography;



namespace _25948083_Wassenaar_L_Project_3.Models
{
    public class UserModel
    {
        [Display(Name = "USERNAME")]
        [Required(ErrorMessage = "Username is required")]
        [StringLength(25, ErrorMessage = "Must be between 3 and 25 characters", MinimumLength = 3)]
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

        //encrypt password
        public string hash(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));

            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}