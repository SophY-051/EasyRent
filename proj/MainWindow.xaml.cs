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
using static System.Net.Mime.MediaTypeNames;


namespace EasyRentProj
{

    public partial class MainWindow : MetroWindow
    {

        //RA 22.04.2025 Login Screen erstellt
        public MainWindow()
        {
            InitializeComponent();
         
        }

        //RA 22.04.2025 Button für den Login
        private void bLogIn_Click(object sender, RoutedEventArgs e)
        {

            var Username = tbBenutzername.Text;
            var Password = pbPasswort.Password;

            //RA 22.04.2025 Überprüfung ob der Benutzername und das Passwort in der Datenbank vorhanden sind
            using (UserLoginSQLData context = new UserLoginSQLData())
            {
                bool usergefunden = context.User.Any(user => user.Benutzername == Username && user.Passwort == Password);

                if (usergefunden)
                {
                    Zugriff();
                    Close();
                }
                else
                {
                    MessageBox.Show("Benutzername oder Passwort falsch");
                }
            }

        }

        //RA 22.04.2025 Methode um das Hauptmenü zu öffnen
        public void Zugriff()
        {
            Hauptmenu hauptmenu = new Hauptmenu();
            hauptmenu.Show();
        }

    }

}