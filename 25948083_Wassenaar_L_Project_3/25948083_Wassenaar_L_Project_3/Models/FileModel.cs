using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace _25948083_Wassenaar_L_Project_3.Models
{
    public class FileModel
    {
        public string file;
        public string message;

        [Display(Name = "DESCRIPTION")]
        [Required(ErrorMessage = "Please enter a discription")]
        public string file_descripion { get; set; }

        public string exsiting_new_file(string path)
        {
            if (System.IO.File.Exists(path))
                file = "update";
          
            else
                file = "initial upload";
 
            return file;
        }

        public string exsiting_new_file_message(string path)
        {
           
            if (System.IO.File.Exists(path))
                message = "File has been updated";

            else
                message = "File has been uploaded";
            
            return message;
        }

  
        public string determine_file_size_in_kb(double file_size)
        {
            double to_kb = (file_size / 1024);
            string kb_to_decimal = string.Format("{0:N0}", to_kb);
            return kb_to_decimal;
        }

    }
}
        

