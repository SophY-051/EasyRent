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
    //SR 06.03.2025 DB file für die Auto Registrierung 
    class AutoRegSQLData 
    {
        public static List<Auto> LoadCar()
        {
            //SR 10.03.2025 Zur Absicherung vor Abstürtze -> schließt die DB Verbindung ordentlich 
            using (IDbConnection cnn = new SqliteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Auto>("select * from carReg", new DynamicParameters());
                //SR 10.03.2025 Sql Ausgabe wird als Liste ausgegebn
                return output.ToList();
            }
        }

        private static void SaveCar(Auto carReg)
        {
            using (IDbConnection cnn = new SqliteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into carReg(marke, model) values(@marke, @model)", carReg);
            }
        }

        //SR 10.03.2025 Verbindung mit Connectionstring aus der App.config Datei -> Verbindung zu DB Pfad
        private static string LoadConnectionString(string id = "SQLiteDB")
        { 
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

    }
}
