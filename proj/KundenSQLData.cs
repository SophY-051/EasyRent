using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace EasyRentProj
{
    public static class KundenSQLData
    {
        private static string connectionString = "Data Source=kunden.db;Version=3;";

        public static void InitDatabase()
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                //RA 22.04.2025 Datenbank initialisieren
                conn.Open();
                string sql = @"CREATE TABLE IF NOT EXISTS kunden (
                                kundeID INTEGER PRIMARY KEY AUTOINCREMENT,
                                vorname TEXT,
                                nachname TEXT,
                                nummer INTEGER,
                                adresse TEXT,
                                email TEXT)";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public static List<Kunde> LoadKunden()
        {
            var kunden = new List<Kunde>();

            using (var conn = new SQLiteConnection(connectionString))
            {
                //RA 22.04.2025 Kunden laden
                conn.Open();
                var cmd = new SQLiteCommand("SELECT * FROM kunden", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        kunden.Add(new Kunde
                        {
                            kundeID = reader.GetInt32(0),
                            vorname = reader.GetString(1),
                            nachname = reader.GetString(2),
                            nummer = reader.GetInt32(3),
                            adresse = reader.GetString(4),
                            email = reader.GetString(5)
                        });
                    }
                }
            }

            return kunden;
        }

        public static void SaveKunde(Kunde kunde)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                //RA 22.04.2025 Kunden speichern    
                conn.Open();
                var cmd = new SQLiteCommand("INSERT INTO kunden (vorname, nachname, nummer, adresse, email) VALUES (@v, @n, @nu, @a, @e)", conn);
                cmd.Parameters.AddWithValue("@v", kunde.vorname);
                cmd.Parameters.AddWithValue("@n", kunde.nachname);
                cmd.Parameters.AddWithValue("@nu", kunde.nummer);
                cmd.Parameters.AddWithValue("@a", kunde.adresse);
                cmd.Parameters.AddWithValue("@e", kunde.email);
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteKunde(Kunde kunde)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                //RA 22.04.2025 Kunden löschen
                conn.Open();
                var cmd = new SQLiteCommand("DELETE FROM kunden WHERE kundeID = @id", conn);
                cmd.Parameters.AddWithValue("@id", kunde.kundeID);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
