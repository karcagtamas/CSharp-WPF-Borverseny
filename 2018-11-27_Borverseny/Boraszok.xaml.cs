using _2018_11_27_Borverseny.Data;
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

namespace _2018_11_27_Borverseny
{
    /// <summary>
    /// Interaction logic for Boraszok.xaml
    /// </summary>
    public partial class Boraszok : Window
    {
        public Boraszok()
        {
            InitializeComponent();
            try
            {
                dgLista.ItemsSource = new Adateleres().ListBoraszok("");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HIBA", MessageBoxButton.OK, MessageBoxImage.Error);
                dgLista.ItemsSource = new List<Borasz>(); 
            }
        }

        private void btnKereses_Click(object sender, RoutedEventArgs e)
        {
            dgLista.ItemsSource = new Adateleres().ListBoraszok(txtBoraszNevKeres.Text);
        }

        private void btnUj_Click(object sender, RoutedEventArgs e)
        {
            var ablak = new BoraszReszletek();
            if (ablak.ShowDialog() == true)
            {
                ((List<Borasz>)dgLista.ItemsSource).Add(ablak.Borasz);
                dgLista.Items.Refresh();
            }
        }

        private void btnModositas_Click(object sender, RoutedEventArgs e)
        {
            if (dgLista.SelectedItem != null)
            {
                Borasz elem = (Borasz)dgLista.SelectedItem;
                List<Borasz> lista = (List<Borasz>)dgLista.ItemsSource;
                int index = lista.IndexOf(elem);

                elem = new Adateleres().GetBorasz(elem.Id);
                var ablak = new BoraszReszletek(elem);
                if (ablak.ShowDialog() == true)
                {
                    lista[index] = ablak.Borasz;
                    dgLista.Items.Refresh();
                }
            }
        }

        private void btTorles_Click(object sender, RoutedEventArgs e)
        {
            if (dgLista.SelectedItem != null)
            {
                if (MessageBox.Show("Biztosan törölni szeretné?", "Megerősítése", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Borasz elem = (Borasz)dgLista.SelectedItem;
                    List<Borasz> lista = (List<Borasz>)dgLista.ItemsSource;
                    try
                    {
                        new Adateleres().DeleteBorasz(elem);
                        lista.Remove(elem);
                        dgLista.Items.Refresh();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
