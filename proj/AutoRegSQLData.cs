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
                var output = cnn.Query<Auto>("select * from tAutoReg", new DynamicParameters());
                //SR 10.03.2025 Sql Ausgabe wird als Liste ausgegebn
                return output.ToList();
            }
        }


        public static void SaveCar(Auto autoReg)
        {
            //SR 18.03.2024 Methode um neu erstelle Auto Objekte in der db zu speichern
            using (IDbConnection cnn = new SqliteConnection(LoadConnectionString()))
            {
                //SR 18.03.2024 SQL Abfrage mit @ = Auto eigenschaften
                cnn.Execute("insert into tAutoReg (autoMarke, autoModel,autoGetriebe,autoSitze,autoPreis) values(@autoMarke, @autoModel,@autoGetriebe,@autoSitze,@autoPreis)", autoReg);
            }
        }

        //SR 10.03.2025 Verbindung mit Connectionstring aus der App.config Datei -> Verbindung zu DB Pfad flexibler als so wie in save cars
        private static string LoadConnectionString(string id = "SQLiteDB")
        { 
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        public static void DeleteCar(Auto autoReg)
        {
            //SR 19.03.2025 Methode um Regestrierte Autos aus der Datenbank zu löschen
            using (IDbConnection cnn = new SqliteConnection(LoadConnectionString())) 
            {
                cnn.Execute("DELETE FROM tAutoReg WHERE autoID = @autoID", autoReg);
            }
           
        }
        public static int GetAutoPreis(int autoID)
        {
            //SR 26.03.2025 Methode , um den Preis für ein spezifisches Auto abzurufen
            using (IDbConnection cnn = new SqliteConnection(LoadConnectionString()))
            {
                return cnn.QuerySingle<int>("SELECT autoPreis FROM tAutoReg WHERE autoID = @autoID", new { autoID });
            }
        }


    }
}
