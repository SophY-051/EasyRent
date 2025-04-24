using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EasyRentProj
{
    /// <summary>
    /// Interaktionslogik für Rueckgaben.xaml
    /// </summary>
    public partial class Rueckgaben : MetroWindow
    {
        private ObservableCollection<Rueckgabe> rueckgaben = new ObservableCollection<Rueckgabe>();
        private List<Buchung> laufendeBuchungen = new List<Buchung>();

        public Rueckgaben()
        {
            InitializeComponent();
            LoadReturns();
            LoadLaufendeBuchungen();
            LoadBuchungID();
        }

        private void LoadReturns()
        {
            rueckgaben = new ObservableCollection<Rueckgabe>(RueckgabeSQLData.LoadReturns());
            RueckgabeGridXAML.ItemsSource = rueckgaben;
        }

        private void LoadLaufendeBuchungen()
        {
            // Nur Buchungen laden, bei denen das Auto aktuell *nicht* verfügbar ist (also vermietet)
            var alleBuchungen = BuchungSQLData.LoadBookings();
            laufendeBuchungen = alleBuchungen.Where(b => !b.verfügbarkeit).ToList();

            cbBuchungAuswahl.ItemsSource = laufendeBuchungen;
            cbBuchungAuswahl.DisplayMemberPath = "buchungID";  // Optional: Anzeige
            cbBuchungAuswahl.SelectedValuePath = "buchungID";  // Für .SelectedValue
        }

        private void WireUpReturnList()
        {
            RueckgabeGridXAML.ItemsSource = null;
            RueckgabeGridXAML.ItemsSource = rueckgaben;
        }

        private void bRueckgabeHinzufuegen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbBuchungAuswahl.SelectedItem is not Buchung ausgewählteBuchung)
                {
                    MessageBox.Show("Bitte wählen Sie eine laufende Buchung aus.");
                    return;
                }

                // Validierung der Eingaben
                if (!dpRueckgabeDatum.SelectedDate.HasValue ||
                    string.IsNullOrWhiteSpace(tbKilometerstand.Text) ||
                    string.IsNullOrWhiteSpace(tbTankstand.Text))
                {
                    MessageBox.Show("Bitte geben Sie gültige Rückgabedaten ein.");
                    return;
                }

                Rueckgabe rueckgabe = new Rueckgabe
                {
                    buchungID = ausgewählteBuchung.buchungID,
                    autoID = ausgewählteBuchung.autoID,
                    kundeID = ausgewählteBuchung.kundeID,
                    rueckgabeDatum = dpRueckgabeDatum.SelectedDate.Value,
                    kmstand = Convert.ToInt32(tbKilometerstand.Text),
                    tankstand = Convert.ToInt32(tbTankstand.Text),
                    schaeden = tbSchaeden.Text,
                    bemerkung = tbBemerkungen.Text
                };

                RueckgabeSQLData.SaveReturns(rueckgabe);
                rueckgaben.Add(rueckgabe);
                WireUpReturnList();
                LoadLaufendeBuchungen(); // Buchung nun erledigt, also ComboBox neu laden

                MessageBox.Show("Rückgabe erfolgreich gespeichert!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler bei der Rückgabe: {ex.Message}");
            }
        }


        private void LoadBuchungID()
        {
            var buchungIDs = BuchungSQLData.LoadBookings();
            cbBuchungAuswahl.ItemsSource = buchungIDs;
            cbBuchungAuswahl.DisplayMemberPath = "buchungID"; // Anzeige der Buchungs-ID
            cbBuchungAuswahl.SelectedValuePath = "buchungID"; // Wert für .Selec
        }

        private void bRueckgabeLoeschen_Click(object sender, RoutedEventArgs e)
        {
            var selectedReturns = RueckgabeGridXAML.SelectedItems.Cast<Rueckgabe>().ToList();
            foreach (var rueckgabe in selectedReturns)
            {
                rueckgaben.Remove(rueckgabe);
                RueckgabeSQLData.DeleteReturns(rueckgabe);
            }
            WireUpReturnList();
            LoadLaufendeBuchungen();
        }

        private void bDatenAktualisieren_Click(object sender, RoutedEventArgs e)
        {
            LoadReturns();
            LoadLaufendeBuchungen();
            LoadBuchungID();
        }

        private void RueckgabeGridXAML_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RueckgabeGridXAML.SelectedItem is Rueckgabe selectedRueckgabe)
            {
                cbBuchungAuswahl.SelectedValue = selectedRueckgabe.buchungID;
                tbKilometerstand.Text = selectedRueckgabe.kmstand.ToString();
                tbTankstand.Text = selectedRueckgabe.tankstand.ToString();
                tbSchaeden.Text = selectedRueckgabe.schaeden;
                tbBemerkungen.Text = selectedRueckgabe.bemerkung;
                dpRueckgabeDatum.SelectedDate = selectedRueckgabe.rueckgabeDatum;
            }
        }
    }
}
