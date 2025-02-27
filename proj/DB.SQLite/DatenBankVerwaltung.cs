using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

//SR 27.02.2025 erstellung vom DB Ordner und DB Klasse so das jedes Fenster auf diese zugreifen kann
namespace EasyRentProj.DB.SQLite
{
    //SR 27.02.2025 auf public gestellt damit jede Klasse darauf zugreifen kann
    public class DatenBankVerwaltung
    {
        public string connectionString;
        //Diese Methode erstellt die SQLite-Datenbankdatei und eine Tabelle, falls sie noch nicht existieren
        public void CreateDBandTables()
        {
            //erstellt verbindung zu DB
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                //öffnet die Verindung zur DB
                connection.Open();
                //erstellt Tabelle User wenn noch nicht exestiert
                string createTableQuery = "CREATE TABLE IF NOT EXISTS Users (Id INTEGER PRIMARY KEY, Name TEXT, Age INTEGER)";
                using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
                {
                    //führt den Befehl aus
                    command.ExecuteNonQuery();
                }
            }
        }

        //Diese Methode fügt Daten in die User Tabelle ein
        public void DataInput(string name, int age)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO Users (Name, Age) VALUES (@Name, @Age)";
                using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Age", age);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
