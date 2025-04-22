using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRentProj
{
    //RA 22.04.2025 Klasse für die Datenbankverbindung
    public class UserLoginSQLData : DbContext
    {
        //RA 22.04.2025 Pfad zur SQLite-Datenbank über Umgebungsvariable
        public static string path = ConfigurationManager.AppSettings["RENT_DB_PATH"];

        //RA 22.04.2025 Konfiguration der Datenbankverbindung
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={path}");
        }

        //RA 22.04.2025 DbSet für die User-Tabelle
        public DbSet<User> User { get; set; }

    }
    
}
