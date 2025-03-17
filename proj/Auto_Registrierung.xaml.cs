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

            //SR 17.03.2025 new Auto-Objekt initalisieren
            Auto auto1 = new Auto();
            auto1.autoID = 001;
            auto1.autoMarke = "VW";
            auto1.autoModel = "Golf VI";
            auto1.autoGetriebe = "S";
            auto1.autoSitze = 4;
            auto1.autoPreis = 40;

            CarRegGridXAML.Items.Add(auto1);

            Auto auto2 = new Auto();
            auto1.autoID = 002;
            auto1.autoMarke = "Audi";
            auto1.autoModel = "A1 Sport Paket, 2024";
            auto1.autoGetriebe = "A";
            auto1.autoSitze = 4;
            auto1.autoPreis = 124;

            CarRegGridXAML.Items.Add(auto2);
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
