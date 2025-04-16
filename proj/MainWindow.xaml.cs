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
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        public MainWindow()
        {
            InitializeComponent();
         
        }


        private void bLogIn_Click(object sender, RoutedEventArgs e)
        {

            var Username = tbBenutzername.Text;
            var Password = pbPasswort.Password;

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


        public void Zugriff()
        {
            Hauptmenu hauptmenu = new Hauptmenu();
            hauptmenu.Show();
        }

    }

}