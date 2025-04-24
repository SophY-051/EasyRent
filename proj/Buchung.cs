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
        //SR 26.03.25 Eigenschafen der buchungen 
        public int buchungID { get; set; } 
        public int autoID { get; set; }
        public int kundeID { get; set; }
        public DateTime startDatum { get; set; } 
        public DateTime endDatum { get; set; } 
        public int buchungPreis { get; set; }
        public bool verfügbarkeit { get; set; } // Verfügbarkeit als bool
    }
}
