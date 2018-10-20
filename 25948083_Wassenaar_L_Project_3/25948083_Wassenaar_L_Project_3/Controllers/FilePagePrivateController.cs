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

// used for private files
namespace _25948083_Wassenaar_L_Project_3.Controllers
{
    public class FilePagePrivateController : Controller
    {
        public static string file_message;
        public static bool db_insert_success = true;

        //Checks if an repository exists for the specific user that logged in. Uses session to create repository
        [HttpGet]
        public ActionResult CommitP()
        {
            var create_path = Server.MapPath("~/Private_Uploads/" + Session["username"]);
            if (!System.IO.File.Exists(create_path))
            {
                Directory.CreateDirectory(create_path);
            }
            return View();
        }
        // Upload a file, an existing file will be automatically detected. File results is recorded in the database
        [HttpPost]
        public ActionResult CommitP(HttpPostedFileBase file, FileModel file_model, LoginModel login, DbConnection db)
        {
            string file_existing_new = "", file_extension = "", file_size = "", file_name;

            if (file != null)
            {
                file_name = Path.GetFileName(file.FileName);
                file_size = file_model.determine_file_size_in_kb(file.ContentLength);
                file_extension = Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Private_Uploads/" + Session["username"]), file_name);
                file_existing_new = file_model.exsiting_new_file(path);
                file_message = file_model.exsiting_new_file_message(path);

                if (Session["username"] == null)
                    ViewData["Message"] = "User not signed in";
                else
                    insert_upload_info(db.connectionString(), file_name, file_size, file_extension, file_existing_new, file_model, login);

                if (file_model.file_descripion != null && db_insert_success == true)
                    file.SaveAs(path);
            }
            else
                ViewData["Message"] = "Choose a file first";
            return View();
        }
        // List files with their imformation, to be ready for download
        public ActionResult DownloadsP(FileModel file_model,DbConnection db)
        {
            try
            {
                var dir = new DirectoryInfo(Server.MapPath("~/Private_Uploads/" + Session["username"] + "/"));
                FileInfo[] file_names = dir.GetFiles();
                List<string> items = new List<string>();
                int file_count = 0;
                foreach (var file in file_names)
                {
                    DateTime last_edit = file.LastWriteTime;
                    string extension = file.Extension;
                    string file_size = file_model.determine_file_size_in_kb(file.Length);
                    items.Add(file.Name);
                    ViewData[Convert.ToString(file_count)] = "[Last Edit/Upload: " + last_edit + "] [Extension:" + extension + "] [File size:" + file_size + "KB]";
                    file_count++;
                }
                return View(items);
            }
            catch (Exception e) { ViewData["error"] = "File collection error"; return View();}


        }
        // Forces the download of a particular file
        public FileResult DownloadP(string file_name)
        {
            var file_path = "~/Private_Uploads/" + Session["username"] + "/" + file_name;
            return File(file_path, "application/force-download", Path.GetFileName(file_path));
        }
        // insert method of files with their information. Method is called in the pcommit actionresult
        public void insert_upload_info(string connection, string file_name, string file_size, string file_extension, string file_existing_new, FileModel file_model, LoginModel login)
        {
            MySqlConnection sql_con = new MySqlConnection(connection);
            MySqlCommand sql_com = new MySqlCommand("INSERT INTO upload_file_private VALUES(@file_id,@file_name,@file_description,@file_upload_dateTime,@file_size,@file_extension,@file_upload_update,@username)", sql_con);

            try
            {
                sql_con.Open();
                long file_id = sql_com.LastInsertedId;
                sql_com.Parameters.AddWithValue("@file_id", file_id);
                sql_com.Parameters.AddWithValue("@username", Session["username"]);
                sql_com.Parameters.AddWithValue("@file_name", file_name);
                sql_com.Parameters.AddWithValue("@file_description", file_model.file_descripion);
                sql_com.Parameters.AddWithValue("@file_upload_dateTime", DateTime.Now);
                sql_com.Parameters.AddWithValue("@file_size", file_size + "KB");
                sql_com.Parameters.AddWithValue("@file_extension", file_extension);
                sql_com.Parameters.AddWithValue("@file_upload_update", file_existing_new);
                sql_com.ExecuteNonQuery();
                ViewData["Message"] = file_message + "," + file_name + " [DATE: " + DateTime.Now + "]  [File size: " + file_size + "KB] [File Extension: " + file_extension + "]";
                sql_con.Close();
            }
            catch (MySqlException e) { ViewData["Message"] = "File upload error"; db_insert_success = false; }



        }
        // view the history of changes of an existing file
        public ActionResult UploadsP(string file_name, DbConnection db)
        {
            List<UploadModel> list = new List<UploadModel>();
            MySqlConnection sql_con = new MySqlConnection(db.connectionString());
            if (file_name != null)
                ViewData["file_info"] = "Log for " + file_name;

            MySqlCommand sql_com = new MySqlCommand("SELECT * FROM upload_file_private WHERE file_name = @File_name AND username = @Username ORDER BY file_upload_dateTime;", sql_con);
            sql_com.Parameters.AddWithValue("@File_name", file_name);
            sql_com.Parameters.AddWithValue("@Username", Session["username"]);
            sql_con.Open();
            MySqlDataReader reader = sql_com.ExecuteReader();
            while (reader.Read())
            {
                var upload_data = new UploadModel();
                upload_data.file_description = reader["file_description"].ToString();
                upload_data.file_upload_dateTime = reader["file_upload_dateTime"].ToString();
                upload_data.file_size = reader["file_size"].ToString();
                upload_data.file_upload_update = reader["file_upload_update"].ToString();
                list.Add(upload_data);

            }
            return View(list);
        }
        // Delete an existing file
        public ActionResult DeleteP(string file_name)
        {

            var path = Path.Combine(Server.MapPath("~/Private_Uploads/" + Session["username"]), file_name);
            System.IO.File.Delete(path);
            return RedirectToAction("DownloadsP", "FilePagePrivate", new { area = "" });
        }

        // used to display the activity of a specific user
        public ActionResult login_historyP(DbConnection db)
        {
            if (Session["username"] == null)
                ViewData["username"] = "No user logged in";
            else
                ViewData["username"] = "Login activity for " + Session["username"];

            List<login_history_model> list = new List<login_history_model>();
            MySqlConnection sql_con = new MySqlConnection(db.connectionString());
            MySqlCommand sql_com = new MySqlCommand("SELECT login_id,login_date FROM login_history WHERE username = @username ORDER BY login_date;", sql_con);
            sql_com.Parameters.AddWithValue("@username", Session["username"]);
            sql_con.Open();
            MySqlDataReader reader = sql_com.ExecuteReader();
            while (reader.Read())
            {
                var login_hist = new login_history_model();
                login_hist.login_id = reader["login_id"].ToString();
                login_hist.login_date = reader["login_date"].ToString();
                list.Add(login_hist);
            }
            return View(list);

        }

    }
}
