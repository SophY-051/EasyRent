using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace EasyRentProj
{
    // SR 06.03.2025 DB file für die Auto Registrierung 
    class AutoRegSQLData
    {
        // SoRe 31.03.2025 Dynamischer DB-Pfad über Umgebungsvariable oder App.config
        private static string path => GetDatabasePath();

        public static string Path => path;

        private static string GetDatabasePath()
        {
            // Erst Umgebungsvariable lesen
            string envPath = Environment.GetEnvironmentVariable("RENT_DB_PATH");

            // Wenn leer, auf App.config-Fallback gehen
            if (string.IsNullOrEmpty(envPath))
            {
                envPath = ConfigurationManager.AppSettings["RENT_DB_PATH"];
            }

            if (string.IsNullOrEmpty(envPath))
            {
                throw new Exception("Datenbankpfad ist nicht definiert. Bitte 'RENT_DB_PATH' als Umgebungsvariable oder in App.config setzen.");
            }

            return envPath;
        }

        public static List<Auto> LoadCar()
        {
            // SR 10.03.2025 Zur Absicherung vor Abstürzen -> schließt die DB Verbindung ordentlich 
            using (IDbConnection cnn = new SqliteConnection($"Data Source={Path}"))
            {
                var output = cnn.Query<Auto>("select * from tAutoReg", new DynamicParameters());
                return output.ToList(); // SR 10.03.2025 SQL Ausgabe wird als Liste ausgegeben
            }
        }

        public static void SaveCar(Auto autoReg)
        {
            // SR 18.03.2024 Methode um neu erstellte Auto Objekte in der DB zu speichern
            using (IDbConnection cnn = new SqliteConnection($"Data Source={Path}"))
            {
                cnn.Execute("insert into tAutoReg (autoMarke, autoModel,autoGetriebe,autoSitze,autoPreis) values(@autoMarke, @autoModel,@autoGetriebe,@autoSitze,@autoPreis)", autoReg);
            }
        }

        public static void DeleteCar(Auto autoReg)
        {
            // SR 19.03.2025 Methode um registrierte Autos aus der Datenbank zu löschen
            using (IDbConnection cnn = new SqliteConnection($"Data Source={Path}"))
            {
                cnn.Execute("DELETE FROM tAutoReg WHERE autoID = @autoID", autoReg);
            }
        }

        public static int GetAutoPreis(int autoID)
        {
            // SR 26.03.2025 Methode, um den Preis für ein spezifisches Auto abzurufen
            using (IDbConnection cnn = new SqliteConnection($"Data Source={Path}"))
            {
                return cnn.QuerySingle<int>("SELECT autoPreis FROM tAutoReg WHERE autoID = @autoID", new { autoID });
            }
        }
    }
}
