using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace EasyRentProj
{
    public class RueckgabeSQLData : DbContext
    {
        // RA 22.04.2025 Pfad zur SQLite-Datenbank über Umgebungsvariable oder App.config
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

        // RA 22.04.2025 Konfiguration der Datenbankverbindung
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={path}");
        }

        // RA 22.04.2025 DbSet für die Rueckgabe-Tabelle
        public DbSet<Rueckgabe> Rueckgaben { get; set; }
    }
}
