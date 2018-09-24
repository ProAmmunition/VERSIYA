using _25948083_Wassenaar_L_Project_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace _25948083_Wassenaar_L_Project_3.Controllers
{
    public class LoginController : Controller
    {
        public string connection = "datasource = den1.mysql4.gear.host; port=3306; Initial Catalog = 'versiyadb'; username='versiyadb';password='En5KD_989Z-9';";
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginModel login,UserModel user)
        {
            MySqlConnection sql_con = new MySqlConnection(connection);
            string sql_statement = "SELECT username,user_password FROM user WHERE username = @username AND user_password = @user_password;";
            sql_con.Open();
            MySqlCommand sql_com = new MySqlCommand(sql_statement, sql_con);
            sql_com.Parameters.AddWithValue("@username", login.Username);
            sql_com.Parameters.AddWithValue("@user_password", user.hash(login.Password));
            MySqlDataReader dataRead = sql_com.ExecuteReader();
            if(dataRead.Read())
            {
                Session["username"] = login.Username.ToString();
                Session["user_password"] = user.hash(login.Password.ToString());
                return RedirectToAction("Commit", "FilePage", new { area = "" });
            }
            else
                ViewData["message"] = "Invalid login details";
            
            sql_con.Close();
            return View();
        }
    }
}