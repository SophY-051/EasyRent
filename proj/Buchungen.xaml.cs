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
    /// Interaktionslogik für Buchungen.xaml
    /// </summary>
    /// 
    public partial class Buchungen : MetroWindow
    {
        //SR 18.02.2025 Screen erstellt und mit API MahApp verbunden

        private ObservableCollection<Buchung> buchungen = new ObservableCollection<Buchung>();
        public Buchungen()
        {
            InitializeComponent();
            LoadBookings();
            LoadAutoID();
        }


        private void LoadAutoID()
        {
            var autoIDs = AutoRegSQLData.LoadCar();
            cbAutoAuswahl.ItemsSource = autoIDs;
        }
        private void LoadBookings()
        {
            buchungen = new ObservableCollection<Buchung>(BuchungSQLData.LoadBookings());
            WireUpBookingList();
        }

        private void WireUpBookingList()
        {
            BuchungGridXAML.ItemsSource = null; // Datenquelle zurücksetzen
            BuchungGridXAML.ItemsSource = buchungen; // Neue Datenquelle zuweisen
        }

        private void bBuchungHinzufuegen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Beispiel: autoID aus einer Auswahl im Frontend (z. B. ComboBox)
                int autoID = GetSelectedAutoID(); // Funktion holt die ausgewählte AutoID
                if (autoID == 0)
                {
                    MessageBox.Show("Bitte wählen Sie ein Auto aus!");
                    return;
                }

                Buchung buchung = new Buchung
                {
                    autoID = autoID,
                    startDatum = dpStartDatum.SelectedDate.Value,
                    endDatum = dpEndDatum.SelectedDate.Value,
                    buchungPreis = BerechneGesamtPreis(autoID, dpStartDatum.SelectedDate.Value, dpEndDatum.SelectedDate.Value)
                };

                // Speichern in der Datenbank
                BuchungSQLData.SaveBooking(buchung);
                buchungen.Add(buchung);
                WireUpBookingList(); // Aktualisierung des DataGrid
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler: {ex.Message}");
            }
        }

        private int GetSelectedAutoID()
        {
            if (cbAutoAuswahl.SelectedItem != null)
            {
                var selectedAuto = (Auto)cbAutoAuswahl.SelectedItem;
                return selectedAuto.autoID;
            }
            return 0; // Rückgabe von 0, wenn kein Auto ausgewählt ist
        }

        private void bBuchungLoeschen_Click(object sender, RoutedEventArgs e)
        {
            var selectedBookings = BuchungGridXAML.SelectedItems.Cast<Buchung>().ToList();
            foreach (var buchung in selectedBookings)
            {
                buchungen.Remove(buchung);
                BuchungSQLData.DeleteBooking(buchung); // Aus der DB löschen
            }
            WireUpBookingList();
        }
        private void bDatenAktualisieren_Click(object sender, RoutedEventArgs e)
        {
            LoadBookings(); // Buchungen erneut laden
        }

        private int BerechneGesamtPreis(int autoID, DateTime startDatum, DateTime endDatum)
        {
            int autoPreis = AutoRegSQLData.GetAutoPreis(autoID);
            TimeSpan mietDauer = endDatum - startDatum;
            return mietDauer.Days * autoPreis; // Gesamtpreis = Mietdauer * Tagespreis
        }

    }


}
