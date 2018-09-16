using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace _25948083_Wassenaar_L_Project_3.Models
{
    public class FileModel
    {
        [Display(Name = "Description")]
        public string file_descripion { get; set; }

        public string exsiting_new_file(string path)
        {
            string file;
            if (System.IO.File.Exists(path))
            {
                file = "existing";
            }
            else
            {
                file = "new";
            }
            return file;
        }

        public string exsiting_new_file_message(string path)
        {
            string message;
            if (System.IO.File.Exists(path))
            {
                message = "File has been updated";

            }
            else
            {
                message = "File has been uploaded";
            }
            return message;
        }
    }
}
        

