﻿using MahApps.Metro.Controls;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace EasyRentProj
{
    public partial class Buchungen : MetroWindow
    {
        private ObservableCollection<Buchung> buchungen = new ObservableCollection<Buchung>();
        private const decimal Tagespreis = 50m; // Beispiel: 50 Euro pro Tag

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BuchungSQLData buchungSQLData = new BuchungSQLData();
            tbBuchungsID.Text = buchungSQLData.GetNextBuchungsID();
            var kundenListe = KundenSQLData.LoadKunden();
            cbKundeAuswahl.ItemsSource = kundenListe;
            cbKundeAuswahl.DisplayMemberPath = "kundeID";
            cbKundeAuswahl.SelectedValuePath = "kundeID";
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
                int kundeID = Convert.ToInt32(cbKundeAuswahl.SelectedValue);
                int autoID = GetSelectedAutoID();
                int buchungID = Convert.ToInt32(tbBuchungsID.Text);
                if (autoID == 0)
                {
                    MessageBox.Show("Bitte wählen Sie ein Auto aus!");
                    return;
                }

                Buchung buchung = new Buchung
                {
                    buchungID = buchungID,
                    autoID = autoID,
                    kundeID = kundeID,
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

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpStartDatum.SelectedDate.HasValue && dpEndDatum.SelectedDate.HasValue)
            {
                DateTime startDatum = dpStartDatum.SelectedDate.Value;
                DateTime endDatum = dpEndDatum.SelectedDate.Value;

                if (endDatum >= startDatum)
                {
                    int tage = (endDatum - startDatum).Days + 1;
                    decimal gesamtpreis = tage * Tagespreis;
                    tbGesamtPreis.Text = gesamtpreis.ToString("F2");
                }
                else
                {
                    MessageBox.Show("Das Enddatum muss nach dem Startdatum liegen.", "Ungültiger Zeitraum", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbGesamtPreis.Text = string.Empty;
                }
            }
            else
            {
                tbGesamtPreis.Text = string.Empty;
            }
        }
    }
}
