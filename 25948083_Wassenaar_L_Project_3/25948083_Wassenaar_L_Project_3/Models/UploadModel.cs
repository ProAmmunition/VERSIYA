using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace _25948083_Wassenaar_L_Project_3.Models
{
    public class UploadModel
    {
        [Display(Name = "File name")]
        public string file_name { get; set; }

        [Display(Name = "File description")]
        public string file_description { get; set; }

        [Display(Name = "File edit/upload date ")]
        public string file_upload_dateTime { get; set; }

        [Display(Name = "File size")]
        public string file_size { get; set; }

        [Display(Name = "File extension")]
        public string file_extension { get; set; }

        [Display(Name = "Initail Upload/Update")]
        public string file_upload_update { get; set; }
    }
}