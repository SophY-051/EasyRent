using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRentProj
{
    //SR 06.03.2025 DB file für die Auto Registrierung 
    class AutoRegSQLData
    {


        //SR 06.03.2025
        private static string LoadConnectionString(string id = "SQLiteDB")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

    }
}
