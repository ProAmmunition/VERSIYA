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
                var file_name = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Uploads"), file_name);
                file.SaveAs(path);
                ViewData["Message"] = "File upload successful";
            }
            else
            {
                ViewData["Message"] = "File upload error";
            }
            return View();

        }
    }
}
