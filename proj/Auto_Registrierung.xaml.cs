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
    /// Interaktionslogik für Auto_Registrierung.xaml
    /// </summary>
    /// 


    public partial class Auto_Registrierung : MetroWindow
    {
        //SR 18.02.2025 Screen erstellt und mit API MahApp verbunden
        //SR 18.02.2025 Screen erstellt und mit API MahApp verbunden
        /*private void TextBox_TextChanged()
        {

        }*/
        public Auto_Registrierung()
        {
            InitializeComponent();

            //SR 17.03.2025 new Auto-Objekt initalisiere 

        }


        private void bAutoHinzufuegen_Click(object sender, RoutedEventArgs e)
        {
            //SR 18.03.2025 Mit dem Button Auto hinzufügen wird ein temporeres Auto Object erstellt und damit kann dann ein neues Auto hinzugefügt werden
            Auto tempAuto = new Auto();
            tempAuto.autoID = tbAutoID.Text;
            tempAuto.autoMarke = tbAutoMarke.Text;
            tempAuto.autoModel = tbAutoModel.Text;
            tempAuto.autoGetriebe = tbGetriebe.Text;
            tempAuto.autoSitze = tbSitze.Text;
            tempAuto.autoPreis = tbPreis.Text;

            CarRegGridXAML.Items.Add(tempAuto);
        }

            /*SR 18.02.2025 Variabeln die alle wichtigen Daten für die Autoregistrierung speichert
            public string registratedCar
                {
                    get
                    {
                    return $"{Marke} {Model}";
                    }

                } */



        }
    }
