using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRentProj
{
    class KundenVerSQLData
    {
        //SoRe 31.03.2025 Systemvariabel: RENT_DB_PATH erstellt so das jeder Benutzer sein eigenen Speicherort für die DB aufrufen kann
        private static string path = ConfigurationManager.AppSettings["RENT_DB_PATH"];

        public static string Path { get => path; set => path = value; }

        public static List<Kunde> LoadCar()
        {
            //SR 10.03.2025 Zur Absicherung vor Abstürtze -> schließt die DB Verbindung ordentlich 
            using (IDbConnection cnn = new SqliteConnection($"Data Source={Path}"))
            {
                var output = cnn.Query<Kunde>("select * from tKundenVer", new DynamicParameters());
                //SR 10.03.2025 Sql Ausgabe wird als Liste ausgegebn
                return output.ToList();
            }
        }
    }

}