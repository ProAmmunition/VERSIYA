using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Web.Routing;
using _25948083_Wassenaar_L_Project_3.Models;

//used for user registration
namespace _25948083_Wassenaar_L_Project_3.Controllers
{
    public class RegistrationController : Controller
    {
        public ActionResult Registration_page()
        {
            return View();
        }

        // add a user, a unique index is used in the database to detect if the user infromation already exists
        [HttpPost]
        public ActionResult Registration_page(UserModel user_model, DbConnection db)
        {
            MySqlConnection sql_con = new MySqlConnection(db.connectionString());
            MySqlCommand sql_com = new MySqlCommand("INSERT INTO user VALUES(@username,@user_password,@user_email_address)", sql_con);
                try
                    {
                        sql_con.Open();
                        sql_com.Parameters.AddWithValue("@username", user_model.Username);
                        sql_com.Parameters.AddWithValue("@user_password", user_model.hash(user_model.Password));
                        sql_com.Parameters.AddWithValue("@user_email_address", user_model.Email);
                        sql_com.ExecuteNonQuery();
                        ViewData["Message"] = "User was successfully registered";
                        sql_con.Close();
                        return RedirectToAction("Index", "Login", new { area = "" });
                    }
                    catch (MySqlException e){ViewData["Message"] = "Error, please make sure the password,username and Email entries does not already exist";}
                    return View(user_model);
        }
    }
}