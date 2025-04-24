using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Dapper;
using System.Data;
using Microsoft.Data.Sqlite;

namespace EasyRentProj
{
    public class RueckgabeSQLData : DbContext
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

        public static List<Rueckgabe> LoadReturns()
        {
            using (IDbConnection cnn = new SqliteConnection($"Data Source={path}"))
            {
                var output = cnn.Query<Rueckgabe>("SELECT * FROM tRueckgabe", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void DeleteReturns(Rueckgabe rueckgabe)
        {
            using (IDbConnection cnn = new SqliteConnection($"Data Source={path}"))
            {
                cnn.Execute("DELETE FROM tRueckgabe WHERE rueckgabeID = @rueckgabeID", rueckgabe);
            }
        }

        public static void SaveReturns(Rueckgabe rueckgabe)
        {
            using (IDbConnection cnn = new SqliteConnection($"Data Source={path}"))
            {
                // Überprüfen, ob die Buchung existiert und verfügbar ist  
                var buchung = cnn.QueryFirstOrDefault<Buchung>(
                    "SELECT * FROM tBuchungen WHERE buchungID = @buchungID",
                    new { buchungID = rueckgabe.buchungID });

                // Wenn keine Buchung gefunden wird, eine detaillierte Fehlermeldung ausgeben  
                if (buchung == null || buchung.verfügbarkeit)
                {
                    string verfuegbarkeitStatus = buchung == null ? "Buchung nicht gefunden" : buchung.verfügbarkeit.ToString();
                    throw new Exception($"Die Rückgabe kann nicht gespeichert werden. Entweder existiert die Buchung mit der ID {rueckgabe.buchungID} nicht oder das Auto ist nicht verfügbar. Verfügbarkeitsstatus: {verfuegbarkeitStatus}");
                }

                // Kunden-ID aus der Buchung übernehmen, falls sie in der Rückgabe nicht gesetzt ist  
                if (rueckgabe.kundeID == 0)
                {
                    rueckgabe.kundeID = buchung.kundeID;
                }

                // Rückgabe speichern  
                cnn.Execute(
                    "INSERT INTO tRueckgabe (kmstand, tankstand, schaeden, bemerkung, rueckgabeDatum, buchungID, kundeID, autoID) VALUES (@kmstand, @tankstand, @schaeden, @bemerkung, @rueckgabeDatum, @buchungID, @kundeID)",
                    rueckgabe);

                // Verfügbarkeit der Buchung aktualisieren  
                cnn.Execute(
                    "UPDATE tBuchungen SET verfügbarkeit = true WHERE buchungID = @buchungID",
                    new { buchungID = rueckgabe.buchungID });
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={path}");
        }

        public DbSet<Rueckgabe> Rueckgaben { get; set; }
    }
}
