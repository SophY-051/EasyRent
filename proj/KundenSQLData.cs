using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace EasyRentProj
{
    public class KundenSQLData : DbContext
    {
        // RA 22.04.2025 Pfad zur SQLite-Datenbank über Umgebungsvariable oder App.config
        private static string path => GetDatabasePath();

        private static string GetDatabasePath()
        {
            // Erst Umgebungsvariable prüfen
            string envPath = Environment.GetEnvironmentVariable("RENT_DB_PATH");

            // Fallback auf App.config
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={path}");
        }


        // RA 22.04.2025 DbSet für die Kunden-Tabelle
        public DbSet<Kunde> Kunden { get; set; }

        // RA 22.04.2025 Zusammenstellung von der Kunden-Tabelle
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kunde>(entity =>
            {
                entity.HasKey(e => e.kundeID);
                entity.Property(e => e.vorname).HasColumnType("TEXT");
                entity.Property(e => e.nachname).HasColumnType("TEXT");
                entity.Property(e => e.nummer).HasColumnType("INTEGER");
                entity.Property(e => e.adresse).HasColumnType("TEXT");
                entity.Property(e => e.email).HasColumnType("TEXT");
            });
        }

        // RA 22.04.2025 Methoden zum Laden, Speichern und Löschen von Kunden
        public static List<Kunde> LoadKunden()
        {
            using (var db = new KundenSQLData())
            {
                return db.Kunden.ToList();
            }
        }

        public static void SaveKunde(Kunde kunde)
        {
            using (var db = new KundenSQLData())
            {
                db.Kunden.Add(kunde);
                db.SaveChanges();
            }
        }

        public static void DeleteKunde(Kunde kunde)
        {
            using (var db = new KundenSQLData())
            {
                db.Kunden.Remove(kunde);
                db.SaveChanges();
            }
        }

        // RA 22.04.2025 Methode zum Initialisieren der Datenbank
        public static void InitDatabase()
        {
            try
            {
                using (var db = new KundenSQLData())
                {
                    db.Database.EnsureCreated();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Fehler beim Initialisieren der Datenbank: " + ex.Message, ex);
            }
        }
    }
}
