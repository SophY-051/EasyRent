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
    public partial class Hauptmenu : Window
    {
        public Hauptmenu()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Auto_Registrierung auto = new Auto_Registrierung();
            auto.Show();
        
        }
        private void bCustomerManagement_Click(object sender, RoutedEventArgs e)
        {
            Kundenverwaltung kunde = new Kundenverwaltung();
            kunde.Show();

        }
        private void bRenting_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("servus");  
        }   

    }
}