using System.ComponentModel.DataAnnotations;

namespace EasyRentProj
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Benutzername { get; set; }

        public string Passwort { get; set; }
    }
}