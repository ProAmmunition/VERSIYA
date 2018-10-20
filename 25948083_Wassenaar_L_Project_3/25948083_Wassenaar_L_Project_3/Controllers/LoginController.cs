using _25948083_Wassenaar_L_Project_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

// used for the user login
namespace _25948083_Wassenaar_L_Project_3.Controllers
{
    public class LoginController : Controller
    {
     
        public ActionResult Index()
        {
            return View();
        }

        // checks if user input is equal to a registered user
        [HttpPost]
        public ActionResult Index(LoginModel login,UserModel user, DbConnection db)
        {
            MySqlConnection sql_con = new MySqlConnection(db.connectionString());
            sql_con.Open();
            MySqlCommand sql_com = new MySqlCommand("SELECT username,user_password FROM user WHERE username = @username AND user_password = @user_password;", sql_con);
            sql_com.Parameters.AddWithValue("@username", login.Username);
            sql_com.Parameters.AddWithValue("@user_password", user.hash(login.Password));
            MySqlDataReader dataRead = sql_com.ExecuteReader();
            if(dataRead.Read())
            {
                Session["username"] = login.Username.ToString();
                Session["user_password"] = user.hash(login.Password.ToString());
                insert_login(db);
                return RedirectToAction("Public_Private", "FileUploadType", new { area = "" });
            }
            else
                ViewData["message"] = "Invalid login details";
            sql_con.Close();
            return View();
        }

        // records user activity with each user loggin
        public void insert_login(DbConnection db)
        {
            using (MySqlConnection sql_con = new MySqlConnection(db.connectionString())) // used "using" to prevent two queries using the same connection clashing
            {
                using (MySqlCommand sql_com = new MySqlCommand("INSERT INTO login_history VALUES(@login_id,@username,@login_date)", sql_con))
                {
                    try
                    {
                        sql_con.Open();
                        long login_id = sql_com.LastInsertedId;
                        sql_com.Parameters.AddWithValue("@login_id", login_id);
                        sql_com.Parameters.AddWithValue("@username", Session["username"]);
                        sql_com.Parameters.AddWithValue("@login_date", DateTime.Now);
                        sql_com.ExecuteNonQuery();

                    }
                    catch (MySqlException e) { ViewData["message"] = "Error inserting data in log login-history"; }
                }
            }
        }      
    }
}