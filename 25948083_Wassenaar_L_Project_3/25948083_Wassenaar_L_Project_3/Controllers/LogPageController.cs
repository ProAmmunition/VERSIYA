using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Web.Routing;
using _25948083_Wassenaar_L_Project_3.Models;


namespace _25948083_Wassenaar_L_Project_3.Controllers
{
    public class LogPageController : Controller
    {
        public string connection = "datasource = den1.mysql4.gear.host; port=3306; Initial Catalog = 'versiyadb'; username='versiyadb';password='En5KD_989Z-9';";
        public static string file_name = "", file_existing_new = "", file_extension = "", mb_to_decimal = "", file_message = "";
        public static double file_size, to_mb;
        [HttpGet]
        public ActionResult Commit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Commit(HttpPostedFileBase file, FileModel file_model)
        {
           if (file != null)
            {
                file_name = Path.GetFileName(file.FileName);
                file_size = file.ContentLength;
                to_mb = (file_size / 1024) / 1024;
                mb_to_decimal = string.Format("{0:N2}", to_mb);
                file_extension = Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Uploads"), file_name);

                file_existing_new = file_model.exsiting_new_file(path);
                file_message = file_model.exsiting_new_file_message(path);
                file.SaveAs(path);
            }
            else
            {
                ViewData["Message"] = "Choose a file first";
            }
            dbConnection(connection,file_model);
          
            return View();
        }

        public ActionResult Downloads()
        {
            var dir = new DirectoryInfo(Server.MapPath("~/Uploads/"));
            FileInfo[] file_names = dir.GetFiles();
            List<string> items = new List<string>();
            foreach (var file in file_names)
            {
                items.Add(file.Name);
            }
            return View(items);
        }

        public FileResult Download(string file_name)
        {
            var file_path = "~/Uploads/" + file_name;
            return File(file_path, "application/force-download", Path.GetFileName(file_path));
        }

        public void dbConnection(string connection,FileModel file_model)
        {
            

            using (MySqlConnection sql_con = new MySqlConnection(connection))
            {
                string sql_statement = "INSERT INTO upload_file VALUES(@file_id,@file_name,@file_description,@file_upload_dateTime,@file_size,@file_extension,@file_existing_new)";
                using (MySqlCommand sql_com = new MySqlCommand(sql_statement))
                {
                    try
                    {
                        Random ran = new Random();
                        int file_id = ran.Next(10000000, 99999999);
                        sql_com.Connection = sql_con;
                        sql_con.Open();
                        sql_com.Parameters.AddWithValue("@file_id", file_id);
                        sql_com.Parameters.AddWithValue("@file_name", file_name);
                        sql_com.Parameters.AddWithValue("@file_description", file_model.file_descripion);
                        sql_com.Parameters.AddWithValue("@file_upload_dateTime", DateTime.Now.ToString("MM / dd / yyyy hh: mm tt"));
                        sql_com.Parameters.AddWithValue("@file_size", mb_to_decimal);
                        sql_com.Parameters.AddWithValue("@file_extension", file_extension);
                        sql_com.Parameters.AddWithValue("@file_existing_new", file_existing_new);
                        sql_com.ExecuteNonQuery();
                        ViewData["Message"] = file_message + "," + file_name + " [DATE: " + DateTime.Now.ToString("MM / dd / yyyy hh: mm tt") + "] [File size: " + mb_to_decimal + "MB] [File Extension: " + file_extension + "]";
                        sql_con.Close();
                    }
                    catch (MySqlException e)
                    {
                        ViewData["Message"] = e.Message;
                    }
                }
            }

        }

    }
}
