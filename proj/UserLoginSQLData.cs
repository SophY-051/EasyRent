using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;

namespace EasyRentProj
{
    // RA 22.04.2025 Klasse für die Datenbankverbindung
    public class UserLoginSQLData : DbContext
    {
        // RA 22.04.2025 Konfiguration der Datenbankverbindung
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Erst Umgebungsvariable lesen
            string dbPath = Environment.GetEnvironmentVariable("RENT_DB_PATH");

            // Falls nicht gesetzt, auf App.config-Fallback gehen
            if (string.IsNullOrEmpty(dbPath))
            {
                dbPath = ConfigurationManager.AppSettings["RENT_DB_PATH"];
            }

            if (string.IsNullOrEmpty(dbPath))
            {
                throw new Exception("Datenbankpfad ist nicht definiert. Bitte 'RENT_DB_PATH' setzen.");
            }

            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        // RA 22.04.2025 DbSet für die User-Tabelle
        public DbSet<User> User { get; set; }
    }
}
