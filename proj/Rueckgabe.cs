using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Configuration; 

namespace EasyRentProj
{
    public class Rueckgabe
    {
        public int rueckgabeId { get; set; }

        public int kmstand { get; set; }

        public int tankstand { get; set; }

        public string schaeden { get; set; }

        public string bemerkung { get; set; }
        public DateTime rueckgabeDatum { get; set; }
        public int kundeID { get; set; }
        public int buchungID { get; set; }
        public int autoID { get; set; }
    }
}
