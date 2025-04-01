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
    //SR 26.03.2025 DB file für Buchungen erstellt
    class BuchungSQLData
    {
        public static string path = ConfigurationManager.AppSettings["RENT_DB_PATH"];

        public static List<Buchung> LoadBookings()
        {
            using (IDbConnection cnn = new SqliteConnection($"Data Source={path}"))
            {
                //SR 26.03.2025 Methode, um alle Buchungen zu laden
                var output = cnn.Query<Buchung>("SELECT * FROM tBuchungen", new DynamicParameters());
                return output.ToList();
            }
        }

        public static bool CheckVerfügbarkeit(int autoID, DateTime startDatum, DateTime endDatum)
        {
            using (IDbConnection cnn = new SqliteConnection($"Data Source={path}"))
            {
                var existingBookings = cnn.Query<Buchung>("SELECT * FROM tBuchungen WHERE autoID = @autoID AND ((startDatum <= @endDatum AND endDatum >= @startDatum))", new { autoID, startDatum, endDatum });
                return !existingBookings.Any(); // Verfügbarkeit ist gegeben, wenn keine bestehenden Buchungen im Zeitraum vorhanden sind
            }
        }
        public static void SaveBooking(Buchung buchung)
        {
            using (IDbConnection cnn = new SqliteConnection($"Data Source={path}"))
            {
                if (CheckVerfügbarkeit(buchung.autoID, buchung.startDatum, buchung.endDatum))
                {
                    buchung.verfügbarkeit = true;
                    cnn.Execute("INSERT INTO tBuchungen (startDatum, endDatum, buchungPreis, autoID, verfügbarkeit) VALUES (@startDatum, @endDatum, @buchungPreis, @autoID, @verfügbarkeit)", buchung);
                }
                else
                {
                    throw new Exception("Das Auto ist im gewählten Zeitraum nicht verfügbar.");
                }
            }
        }

        public static void DeleteBooking(Buchung buchung)
        {
            //SR 26.03.2025 Methode um Buchungen aus der Datenbank zu löschen
            using (IDbConnection cnn = new SqliteConnection($"Data Source={path}"))
            {
                cnn.Execute("DELETE FROM tBuchungen WHERE buchungID = @buchungID", buchung);
            }
        }
    }
}