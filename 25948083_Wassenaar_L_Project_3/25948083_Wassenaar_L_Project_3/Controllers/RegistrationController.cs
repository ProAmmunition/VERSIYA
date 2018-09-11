using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Web.Routing;

namespace _25948083_Wassenaar_L_Project_3.Controllers
{
    public class RegistrationController : Controller
    {
        public string connection = "datasource = den1.mysql4.gear.host; port=3306; Initial Catalog = 'versiyadb'; username='versiyadb';password='En5KD_989Z-9';";
        public ActionResult Registration_page()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Registration_page(Models.UserModel user_model)
        {

            using (MySqlConnection sql_con = new MySqlConnection(connection))
            {


                string sql_statement = "INSERT INTO user VALUES(@user_id,@username,@user_password,@user_email_address)";
                using (MySqlCommand sql_com = new MySqlCommand(sql_statement))
                {
                    try
                    {
                        sql_com.Connection = sql_con;
                        sql_con.Open();
                        sql_com.Parameters.AddWithValue("@user_id", user_model.User_id);
                        sql_com.Parameters.AddWithValue("@username", user_model.Username);
                        sql_com.Parameters.AddWithValue("@user_password", user_model.hash(user_model.Password));
                        sql_com.Parameters.AddWithValue("@user_email_address", user_model.Email);
                        sql_com.ExecuteNonQuery();
                        ViewData["Message"] = "User was successfully registered";
                        sql_con.Close();
                        return RedirectToAction("Index", "Login", new { area = "" });
                    }
                    catch (MySqlException e)
                    {
                       
                            ViewData["Message"] = "Error, please make sure the ID, username and Email entries does not already exist";
                        
                    }
                    return View(user_model);
                }


            }
        }
    }
}