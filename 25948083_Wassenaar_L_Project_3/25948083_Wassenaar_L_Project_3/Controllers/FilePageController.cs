﻿using System;
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
    public class FilePageController : Controller
    {
        public static string file_message;

        [HttpGet]
        public ActionResult Commit()
        {
            @TempData.Keep("get_username");
            return View();
        }

        [HttpPost]
        public ActionResult Commit(HttpPostedFileBase file, FileModel file_model, LoginModel login, DbConnection db)
        {
          string file_existing_new = "", file_extension = "", file_size = "", file_name;

            if (file != null || TempData["get_username"] != null)
            {
                file_name = Path.GetFileName(file.FileName);
                file_size = file_model.determine_file_size_in_mb(file.ContentLength);
                file_extension = Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Uploads"), file_name);
                file_existing_new = file_model.exsiting_new_file(path);
                file_message = file_model.exsiting_new_file_message(path);
                insert_upload_info(db.connectionString(), file_name, file_size, file_extension, file_existing_new, file_model, login);
                if(file_model.file_descripion != null)
                file.SaveAs(path);
            }
            else
                ViewData["Message"] = "Choose a file first";
                return View();
        }

        public ActionResult Downloads(FileModel file_model)
        {
            var dir = new DirectoryInfo(Server.MapPath("~/Uploads/"));
            FileInfo[] file_names = dir.GetFiles();
            List<string> items = new List<string>();
            int file_count = 0;
            foreach (var file in file_names)
            {
               DateTime last_edit = file.LastWriteTime;
               string extension = file.Extension;
               string file_size = file_model.determine_file_size_in_mb(file.Length);
               items.Add(file.Name);
               ViewData[Convert.ToString(file_count)] = "[Last Edit: " + last_edit + "] [Extension:" + extension + "] [File size:" + file_size + "mb]";
               file_count++;
            }
            return View(items);
        }

        public FileResult Download(string file_name)
        {
            var file_path = "~/Uploads/" + file_name;
            return File(file_path, "application/force-download", Path.GetFileName(file_path));
        }

        public void insert_upload_info(string connection,string file_name,string file_size,string file_extension,string file_existing_new,FileModel file_model,LoginModel login)
        {
            MySqlConnection sql_con = new MySqlConnection(connection);
            string sql_statement_insert = "INSERT INTO upload_file VALUES(@file_id,@file_name,@file_description,@file_upload_dateTime,@file_size,@file_extension,@file_upload_update,@username)";
            MySqlCommand sql_com = new MySqlCommand(sql_statement_insert, sql_con);
                
                    try
                    {
                        sql_con.Open();
                        TempData.Keep("get_username");
                        sql_com.Parameters.AddWithValue("@username", TempData["get_username"]);
                        sql_com.Parameters.AddWithValue("@file_id", file_model.generate_file_id());
                        sql_com.Parameters.AddWithValue("@file_name", file_name);
                        sql_com.Parameters.AddWithValue("@file_description", file_model.file_descripion);
                        sql_com.Parameters.AddWithValue("@file_upload_dateTime", DateTime.Now);
                        sql_com.Parameters.AddWithValue("@file_size", file_size+"mb");
                        sql_com.Parameters.AddWithValue("@file_extension", file_extension);
                        sql_com.Parameters.AddWithValue("@file_upload_update", file_existing_new);
                        sql_com.ExecuteNonQuery();
                        ViewData["Message"] = file_message + "," + file_name + " [DATE: " + DateTime.Now + "]  [File size: " + file_size + "MB] [File Extension: " + file_extension + "]";
                        sql_con.Close();
                    }
                    catch (MySqlException e){ ViewData["Message"] = "File upload error";}
                
            

        }

        public ActionResult Uploads(string file_name, DbConnection db)
        {
            List<UploadModel> list = new List<UploadModel>();
            MySqlConnection sql_con = new MySqlConnection(db.connectionString());
            string sql_statement = "SELECT * FROM upload_file WHERE file_name = @File_name ORDER BY file_upload_dateTime;";
                if (file_name != null)
                    ViewData["file_info"] = "Log for " + file_name;
           
             MySqlCommand sql_com = new MySqlCommand(sql_statement,sql_con);
             sql_com.Parameters.AddWithValue("@File_name", file_name);
             sql_con.Open();
             MySqlDataReader reader = sql_com.ExecuteReader();
                while (reader.Read())
                {
                    var upload_data = new UploadModel();
                    upload_data.upload_username = reader["username"].ToString();
                    upload_data.file_description = reader["file_description"].ToString();
                    upload_data.file_upload_dateTime = reader["file_upload_dateTime"].ToString();
                    upload_data.file_size = reader["file_size"].ToString();
                    upload_data.file_upload_update= reader["file_upload_update"].ToString();
                    list.Add(upload_data);
                    
                }
                    return View(list);
        }

    }
}