using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace EasyRentProj
{
    class BuchungSQLData
    {
        private static string path => GetDatabasePath();

        private static string GetDatabasePath()
        {
            string envPath = Environment.GetEnvironmentVariable("RENT_DB_PATH");
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

        public static List<Buchung> LoadBookings()
        {
            using (IDbConnection cnn = new SqliteConnection($"Data Source={path}"))
            {
                var output = cnn.Query<Buchung>("SELECT * FROM tBuchungen", new DynamicParameters());
                return output.ToList();
            }
        }

        public static Buchung GetSelectedBuchungID(int buchungID)
        {
            using (IDbConnection cnn = new SqliteConnection($"Data Source={path}"))
            {
                return cnn.QueryFirstOrDefault<Buchung>("SELECT * FROM tBuchungen WHERE buchungID = @buchungID", new { buchungID });
            }
        }

        public static bool CheckVerfügbarkeit(int autoID, DateTime startDatum, DateTime endDatum)
        {
            using (IDbConnection cnn = new SqliteConnection($"Data Source={path}"))
            {
                var existingBookings = cnn.Query<Buchung>(
                    "SELECT * FROM tBuchungen WHERE autoID = @autoID AND ((startDatum <= @endDatum AND endDatum >= @startDatum))",
                    new { autoID, startDatum, endDatum }
                );

                return !existingBookings.Any();
            }
        }

        public static void SaveBooking(Buchung buchung)
        {
            using (IDbConnection cnn = new SqliteConnection($"Data Source={path}"))
            {
                if (CheckVerfügbarkeit(buchung.autoID, buchung.startDatum, buchung.endDatum))
                {
                    buchung.verfügbarkeit  = false; // Auto wird gebucht, also nicht verfügbar
                    cnn.Execute(
                        "INSERT INTO tBuchungen (startDatum, endDatum, buchungPreis, autoID, kundeID, verfügbarkeit ) " +
                        "VALUES (@startDatum, @endDatum, @buchungPreis, @autoID, @kundeID, @verfügbarkeit )",
                        buchung
                    );
                }
                else
                {
                    throw new Exception("Das Auto ist im gewählten Zeitraum nicht verfügbar.");
                }
            }
        }

        public static void DeleteBooking(Buchung buchung)
        {
            using (IDbConnection cnn = new SqliteConnection($"Data Source={path}"))
            {
                cnn.Execute("DELETE FROM tBuchungen WHERE buchungID = @buchungID", buchung);
            }
        }
    }
}
