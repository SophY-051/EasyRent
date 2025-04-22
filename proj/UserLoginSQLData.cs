using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRentProj
{
    public class UserLoginSQLData : DbContext

    {
        public static string path = ConfigurationManager.AppSettings["RENT_DB_PATH"];

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={path}");
        }

        public DbSet<User> User { get; set; }

    }
    
}
