using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRentProj
{
    public class KundenSQLData
    {
        public static List<Kunde> LoadKunden()
        {
            // Datenbankabfrage hier
            return new List<Kunde>();
        }

        public static void SaveKunde(Kunde kunde)
        {
            // Speichern des Kunden in der DB
        }

        public static void DeleteKunde(Kunde kunde)
        {
            // Löschen des Kunden aus der DB
        }
    }
}
