using System.ComponentModel.DataAnnotations;

namespace EasyRentProj
{
    //RA 22.04.2025 Klasse für den Benutzer für die Datenbank
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Benutzername { get; set; }
        public string Passwort { get; set; }
    }
}