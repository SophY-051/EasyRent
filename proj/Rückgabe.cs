using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Configuration; 

namespace EasyRentProj
{
    public class Rückgabe
    {
        public int Id { get; set; }

        public string Fahrzeug { get; set; }

        public string Kunde { get; set; }

        public int Kmstand { get; set; }

        public int Tankstand { get; set; }

        public string Schaeden { get; set; }

        public string Bemerkung { get; set; }

        public DateTime RueckgabeDatum { get; set; }
    }
}
