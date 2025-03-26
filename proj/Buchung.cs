using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasyRentProj
{
    class Buchung
    {
        public int buchungID { get; set; } 
        public int autoID { get; set; } 
        public DateTime startDatum { get; set; } 
        public DateTime endDatum { get; set; } 
        public int buchungPreis { get; set; } 
        public int verfügbarkeit { get; set; }
    }
}
