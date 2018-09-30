using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _25948083_Wassenaar_L_Project_3.Models
{
    public class DbConnection
    {
        public string connectionString()
        {
            return "datasource = den1.mysql4.gear.host; port=3306; Initial Catalog = 'versiyadb'; username='versiyadb';password='En5KD_989Z-9';";
        }
    }
}