using MahApps.Metro.Controls;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace EasyRentProj
{
    public partial class Buchungen : MetroWindow
    {
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
            BuchungGridXAML.ItemsSource = null;
            BuchungGridXAML.ItemsSource = buchungen;
        }

        private void bBuchungHinzufuegen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int autoID = GetSelectedAutoID();
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

                BuchungSQLData.SaveBooking(buchung);
                buchungen.Add(buchung);
                WireUpBookingList();
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
            return 0;
        }

        private void bBuchungLoeschen_Click(object sender, RoutedEventArgs e)
        {
            var selectedBookings = BuchungGridXAML.SelectedItems.Cast<Buchung>().ToList();
            foreach (var buchung in selectedBookings)
            {
                buchungen.Remove(buchung);
                BuchungSQLData.DeleteBooking(buchung);
            }
            WireUpBookingList();
        }

        private void bDatenAktualisieren_Click(object sender, RoutedEventArgs e)
        {
            LoadBookings();
        }

        private int BerechneGesamtPreis(int autoID, DateTime startDatum, DateTime endDatum)
        {
            int autoPreis = AutoRegSQLData.GetAutoPreis(autoID);
            TimeSpan mietDauer = endDatum - startDatum;
            return mietDauer.Days * autoPreis;
        }

        private void BuchungGridXAML_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BuchungGridXAML.SelectedItem is Buchung selectedBuchung)
            {
                tbBuchungsID.Text = selectedBuchung.buchungID.ToString();
                cbAutoAuswahl.SelectedItem = selectedBuchung.autoID;
                dpStartDatum.SelectedDate = selectedBuchung.startDatum;
                dpEndDatum.SelectedDate = selectedBuchung.endDatum;
                tbGesamtPreis.Text = selectedBuchung.buchungPreis.ToString();
            }
        }

        //SoRe 24.04.2025 Methode um Registrierte Kunden in der Combobox auzuwählen
        private string GetSelectedKundenName()
        {
            if (cbKundeAuswahl.SelectedItem != null)
            {
                var selectedKunde = (Kunde)cbKundeAuswahl.SelectedItem;
                return $"{selectedKunde.vorname} {selectedKunde.nachname}";
            }
            return string.Empty;
        }
    }
}