using MahApps.Metro.Controls;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EasyRentProj
{
    public partial class KundenVerwaltung : MetroWindow
    {
        private ObservableCollection<Kunde> kunden = new ObservableCollection<Kunde>();

        public KundenVerwaltung()
        {
            InitializeComponent();
            KundenSQLData.InitDatabase();
            LoadKunden();
        }

         private void LoadKunden()
          {
           kunden = new ObservableCollection<Kunde>(KundenSQLData.LoadKunden());
           WireUpKundenList();
          }

        private void WireUpKundenList()
        {
            KundenGridXAML.ItemsSource = null;
            KundenGridXAML.ItemsSource = kunden;
        }

          private void bKundeHinzufuegen_Click(object sender, RoutedEventArgs e)
          {
            Kunde kunde = new Kunde();
            try
           {
               if (!tbEmail.Text.Contains("@"))
                {
                    MessageBox.Show("Eine Email muss immer ein '@' enthalten");
                    return;
                }

               kunde.vorname = tbVorname.Text;
               kunde.nachname = tbNachname.Text;
               kunde.nummer = (int)long.Parse(tbNummer.Text);
               kunde.adresse = tbAdresse.Text;
               kunde.email = tbEmail.Text;

               KundenSQLData.SaveKunde(kunde);
               kunden.Add(kunde);
               WireUpKundenList();
               LoadKunden();
           }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Hinzufügen: {ex.Message}");
            }
          }

        private void bKundeLoeschen_Click(object sender, RoutedEventArgs e)
        {
            var selectedKunden = KundenGridXAML.SelectedItems.Cast<Kunde>().ToList();
            foreach (var kunde in selectedKunden)
            {
                kunden.Remove(kunde);
                KundenSQLData.DeleteKunde(kunde);
            }
            WireUpKundenList();
        }

        private void bDatenAktualisieren_Click(object sender, RoutedEventArgs e)
        {
            LoadKunden();
        }

        private void KundenGridXAML_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (KundenGridXAML.SelectedItem is Kunde selectedKunde)
            {
                tbKundenID.Text = selectedKunde.kundeID.ToString();
                tbVorname.Text = selectedKunde.vorname;
                tbNachname.Text = selectedKunde.nachname;
                tbNummer.Text = selectedKunde.nummer.ToString();
                tbAdresse.Text = selectedKunde.adresse;
                tbEmail.Text = selectedKunde.email;
            }
        }

        private void btnSucheKunde_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtSucheKundenId.Text, out int kundenId))
            {
                var ergebnisse = KundenSQLData.SucheKundeNachId(kundenId);
                kunden = new ObservableCollection<Kunde>(ergebnisse);
                WireUpKundenList();

                if (!ergebnisse.Any())
                {
                    MessageBox.Show("Kein Kunde mit dieser ID gefunden");
                }
            }
            else
            {
                MessageBox.Show("Bitte eine gültige Kunden-ID eingeben (Zahl)");
            }
        }

        private void btnAlleAnzeigen_Click(object sender, RoutedEventArgs e)
        {
            txtSucheKundenId.Text = "";
            LoadKunden();
        }
    }
}