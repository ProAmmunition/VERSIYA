using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace _25948083_Wassenaar_L_Project_3.Models
{
    // used to view existing file history with display annotations
    public class UploadModel
    {
        [Display(Name = "FILE NAME")]
        public string file_name { get; set; }

        [Display(Name = "FILE DESCRIPTION")]
        public string file_description { get; set; }

        [Display(Name = "FILE EDIT/UPLOAD DATE")]
        public string file_upload_dateTime { get; set; }

        [Display(Name = "FILE SIZE")]
        public string file_size { get; set; }

        [Display(Name = "FILE EXTENSION")]
        public string file_extension { get; set; }

        [Display(Name = "INITIAL UPLOAD/UPDATE")]
        public string file_upload_update { get; set; }

        [Display(Name = "UPLOADED/UPDATED BY")]
        public string upload_username { get; set; }
    }
}