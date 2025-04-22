using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;

namespace EasyRentProj
{
    public class KundenSQLData : DbContext
    {
        public static string path = ConfigurationManager.AppSettings["RENT_DB_PATH"];

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={path}");
        }

        public DbSet<Kunde> Kunden { get; set; }

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

        // Methoden zur Datenmanipulation

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

        public static void InitDatabase()
        {
            using (var db = new KundenSQLData())
            {
                db.Database.EnsureCreated();
            }
        }
    }
}