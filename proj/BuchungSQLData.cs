using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRentProj
{
    //SR 26.03.2025 DB file für Buchungen erstellt
    class BuchungSQLData
    {

        
        public static List<Buchung> LoadBookings()
        {
            using (IDbConnection cnn = new SqliteConnection(LoadConnectionString()))
            {
                //SR 26.03.2025 Methode, um alle Buchungen zu laden
                var output = cnn.Query<Buchung>("SELECT * FROM tBuchungen", new DynamicParameters());
                return output.ToList();
            }

        }

        
        public static void SaveBooking(Buchung buchung)
        {
            using (IDbConnection cnn = new SqliteConnection(LoadConnectionString()))
            {
                //SR 26.03.2025 Methode mit SQL-Abfrage, um die Buchung zu speichern
                cnn.Execute("INSERT INTO tBuchungen (startDatum, endDatum, buchungPreis, autoID) VALUES (@startDatum, @endDatum, @buchungPreis, @autoID)", buchung);

            }
        }

 
        private static string LoadConnectionString(string id = "SQLiteDB")
        {
            //SR 26.03.2025 Verbindung zu SQLite DB
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        public static void DeleteBooking(Buchung buchung)
        {
            //SR 26.03.2025 Methode um Buchungen aus der Datenbank zu löschen
            using (IDbConnection cnn = new SqliteConnection(LoadConnectionString()))
            {
                cnn.Execute("DELETE FROM tBuchungen WHERE buchungID = @buchungID", buchung);
            }
        }



    }
}
