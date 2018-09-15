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
      
        [HttpGet]
        public ActionResult Commit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Commit(HttpPostedFileBase file)
        {
           if(file != null)
            {
                file_name = Path.GetFileName(file.FileName);
                double file_size = file.ContentLength;
                double to_mb = (file_size / 1024) / 1024;
                string mb_2_decimal = string.Format("{0:N2}", to_mb);
                string file_extension = Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Uploads"), file_name);
                file.SaveAs(path);
                ViewData["Message"] = file_name + " [DATE: " + DateTime.Now.ToString("MM / dd / yyyy hh: mm tt") + "] [File size: " + mb_2_decimal + "MB] [File Extension: " + file_extension + "]";
            }
            else
            {
                ViewData["Message"] = "No file is chosen";
            }
            return View();

        }

        public ActionResult Downloads()
        {
            var dir = new DirectoryInfo(Server.MapPath("~/Uploads/"));
            FileInfo [] file_names = dir.GetFiles();
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
}
}
