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
        //RA 22.04.2025 Pfad zur SQLite-Datenbank über Umgebungsvariable
        public static string path = ConfigurationManager.AppSettings["RENT_DB_PATH"];
        
        //RA 22.04.2025 Konfiguration der Datenbankverbindung
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={path}");
        }
        
        //RA 22.04.2025 DbSet für die Rueckgabe-Tabelle
        public DbSet<Rückgabe> Rueckgaben { get; set; }
    }
}
