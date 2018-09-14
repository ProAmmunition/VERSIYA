using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace _25948083_Wassenaar_L_Project_3.Controllers
{
    public class LogPageController : Controller
    {
        public static string file_name;
        public string file_extension;
        public double file_size;
        public double to_mb;

        [HttpGet]
        public ActionResult Commit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Commit(HttpPostedFileBase file)
        {
           if(file.ContentLength > 0)
            {
                file_name = Path.GetFileName(file.FileName);
                file_size = file.ContentLength;
                to_mb = (file_size / 1024) / 1024;
                string mb_2_decimal = string.Format("{0:N2}", to_mb);
                file_extension = Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Uploads"), file_name);
                file.SaveAs(path);
                ViewData["Message"] = file_name + " [DATE: " + DateTime.Now.ToString("MM / dd / yyyy hh: mm tt") + "] [File size: " + mb_2_decimal + "MB] [File Extension: " + file_extension + "]";
            }
            else
            {
                ViewData["Message"] = "File upload error";
            }
            return View();

        }

        public ActionResult DownloadFile()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";
            byte[] bytes = System.IO.File.ReadAllBytes(path + file_name);
            string fileName = file_name;
            if (file_extension != null || file_name != null)
            {
   
               ViewData["Message"] = file_name;
               return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                
            }
            else
            {
                ViewData["Message"] = "No file was selected";
            }
            return View("Commit");
        }
    }
}
