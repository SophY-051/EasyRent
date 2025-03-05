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


namespace EasyRentProj
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void bLogIn(object sender, RoutedEventArgs e)
        {
            var benutzernameEingabe = tbBenutzername.Text;
            var passwortEingabe = pbPasswort.Password;

                bool korrekteEingabe = benutzernameEingabe == passwortEingabe;

                if (korrekteEingabe)
                {
                    //RA 18.02.2025 Wenn Benutzer korrekt ist, wird das Hauptmenu geöffnet
                    Hauptmenu menu = new Hauptmenu();
                    menu.Show();

                    //RA 26.02.2025 Das Fenster wird geschlossen
                    this.Close();
                }
                else
                {
                    //RA 18.02.2025 Wenn Passwort falsch ist, wird eine Fehlermeldung ausgegeben
                    MessageBox.Show("Falsches Passwort");
                }

        }

    }

}