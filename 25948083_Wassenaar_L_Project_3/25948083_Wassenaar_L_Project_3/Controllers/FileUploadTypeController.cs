using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _25948083_Wassenaar_L_Project_3.Controllers
{
    public class FileUploadTypeController : Controller
    {
        // returns a view to choose private or public file upload
        public ActionResult Public_Private()
        {
            return View();
        }
    }
}