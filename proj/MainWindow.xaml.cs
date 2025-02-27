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
//SR 27.02.2025 Methode zur erstellung der DB hinzufügen
using System.Data.SQLite;


namespace EasyRentProj
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        // Benutze Variablen 
        private string benutzernameEingabe; 
        public MainWindow()
        {
            InitializeComponent();
            //RA 18.02.2025 Passwort wird überprüft

        }
        private void bHauptmenu(object sender, RoutedEventArgs e)
        {
            string benutzernameEingabe = tbBenutzername.Text;


            if (benutzernameEingabe == "SUPERUSER")
            {
                //RA 18.02.2025 Wenn Benutzer korrekt ist, wird das Hauptmenu geöffnet
                Hauptmenu menu = new Hauptmenu();
                menu.Show();
            }
            else
            {
                //RA 18.02.2025 Wenn Passwort falsch ist, wird eine Fehlermeldung ausgegeben
                MessageBox.Show("Falsches Passwort");
            }

        }

    }

}