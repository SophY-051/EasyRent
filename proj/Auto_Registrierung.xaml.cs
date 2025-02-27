using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
//SR 27.02.2025 DB-Klasse hinzugefügt
using EasyRentProj.DB.SQLite;


namespace EasyRentProj
{
    /// <summary>
    /// Interaktionslogik für Auto_Registrierung.xaml
    /// </summary>
    public partial class Auto_Registrierung : MetroWindow
    {
        //SR 18.02.2025 Screen erstellt und mit API MahApp verbunden
        public Auto_Registrierung()
        {
            InitializeComponent();
        }
    }
}
