using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UpdatedScholSystem.Service
{
    public class AppSettings
    {
       
        public static string MySqlConnectionString = ConfigurationManager.AppSettings["mySqlConnectionString"];
    }
}