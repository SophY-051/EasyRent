using MahApps.Metro.Controls;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EasyRentProj
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Hauptmenu : MetroWindow
    {
        //SR 18.02.2025 Screen erstellt und mit API MahApp verbunden, Buttons hinzugefügt und funktion angepasst
        public Hauptmenu()
        {
            InitializeComponent();
        }

        private void bCustomerManagement_Click(object sender, RoutedEventArgs e)
        {
            //Neue Instanz in der variable kundeverwaltung erstellt 
            Kundenverwaltung kundeverwaltung = new Kundenverwaltung();
            //Methode um das Fenster zu öffnen
            kundeverwaltung.Show();

        }

        private void bCarRegistration_Click(object sender, RoutedEventArgs e)
        {
            Auto_Registrierung autoRegistrierung = new Auto_Registrierung();
            autoRegistrierung.Show();

        }

        private void bBooking_Click(object sender, RoutedEventArgs e)
        {
            Buchungen buchen = new Buchungen();
            buchen.Show();
        }

        private void bReturn_Click(object sender, RoutedEventArgs e)
        {
            Rueckgaben rueckgabe = new Rueckgaben();
            rueckgabe.Show();
        }

        private void bLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow logout = new MainWindow();
            logout.Show();
        }


    }
}