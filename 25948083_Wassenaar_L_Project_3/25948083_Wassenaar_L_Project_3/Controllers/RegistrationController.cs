using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace _25948083_Wassenaar_L_Project_3.Controllers
{
    public class RegistrationController : Controller
    {
      
        string connection = "datasource = den1.mysql4.gear.host; port=3306; Initial Catalog = 'versiyadb'; username='versiyadb';password='En5KD_989Z-9';";
        [HttpGet]
        public ActionResult Registration_page(Models.UserModel user_model)
        {
            using (MySqlConnection sql_con = new MySqlConnection(connection))
            {
                sql_con.Open();
                string sql_statement = "INSERT INTO user VALUES(@user_id,@username,@user_password,@user_email_address)";
                MySqlCommand sql_com = new MySqlCommand(sql_statement,sql_con);
                sql_com.Parameters.AddWithValue("@user_id", user_model.user_id);
                sql_com.Parameters.AddWithValue("@username", user_model.username);
                sql_com.Parameters.AddWithValue("@user_password", user_model.password);
                sql_com.Parameters.AddWithValue("@user_email_address", user_model.email);
                sql_com.ExecuteNonQuery();

            }
                return View();
        }
    }
}