using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaktionslogik für Kundenverwaltung.xaml
    /// </summary>
    public partial class Kundenverwaltung : MetroWindow      
    {
        //RA 20.03.2025 Screen erstellt und mit API MahApp verbunden
        public Kundenverwaltung()
        {
            InitializeComponent();

            //RA 20.03.2025 new Kunde-Objekt initalisieren
        }

        private ObservableCollection<Auto> auto = new ObservableCollection<Auto>(); //Globale Variable

        private void LoadCar()
        {
            auto = new ObservableCollection<Auto>(AutoRegSQLData.LoadCar());
            WireUpCarList(); // Aktualisiert die Datenquelle des Grids
        }

        private void WireUpCarList()
        {
            CarRegGridXAML.ItemsSource = null;  // Datenquelle erst zurücksetzen
            CarRegGridXAML.ItemsSource = auto; // Danach neue Datenquelle zuweisen
        }
        private void bDatenAktualisieren_Click(object sender, RoutedEventArgs e)
        {
            LoadCar();
        }

        private void bAutoHinzufuegen_Click(object sender, RoutedEventArgs e)
        {
            //RA 20.03.2025 Mit dem Button Auto hinzufügen wird ein temporeres Auto Object erstellt und damit kann dann ein neues Auto hinzugefügt werden
            Auto a = new Auto();
            try
            {
                a.autoMarke = tbAutoMarke.Text;
                a.autoModel = tbAutoModel.Text;
                a.autoGetriebe = tbGetriebe.Text;
                a.autoSitze = tbSitze.Text;
                a.autoPreis = int.Parse(tbPreis.Text);

                AutoRegSQLData.SaveCar(a); // In die Datenbank speichern
                auto.Add(a); // In die ObservableCollection hinzufügen
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Hinzufügen: {ex.Message}");
            }
        }

        private void bAutoLoeschen_Click(object sender, RoutedEventArgs e)
        {
            //SR 19.03.2025 Button um Regestrierte Autos aus der Datenbank zu löschen
            var selectedCars = CarRegGridXAML.SelectedItems.Cast<Auto>().ToList();
            foreach (var car in selectedCars)
            {
                auto.Remove(car);
                AutoRegSQLData.DeleteCar(car);
            }

        }
    }
}
