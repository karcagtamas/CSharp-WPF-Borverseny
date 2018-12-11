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
    /// Interaction logic for Nevezesek.xaml
    /// </summary>
    public partial class Nevezesek : Window
    {
        public Nevezesek()
        {
            InitializeComponent();
            var kategoriak = new Adateleres().ListKategoriak();
            kategoriak.Insert(0, new Kategoria() { Id = null, Megnevezes = "" });
            cboKategoriaKeres.ItemsSource = kategoriak;
            cboKategoriaKeres.SelectedValuePath = "Id";
            cboKategoriaKeres.DisplayMemberPath = "Megnevezes";

            try
            {
                dgLista.ItemsSource = new Adateleres().ListNevezes(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HIBA", MessageBoxButton.OK, MessageBoxImage.Error);
                dgLista.ItemsSource = new List<Nevezes>();
            }
        }

        private void btnKereses_Click(object sender, RoutedEventArgs e)
        {
            int x;
            dgLista.ItemsSource = new Adateleres().ListNevezes((int?)cboKategoriaKeres.SelectedValue, int.TryParse(txtEvjaratKeres.Text, out x)? (int?)x : null);
        }

        private void btnUj_Click(object sender, RoutedEventArgs e)
        {
            var ablak = new NevezesReszletek();
            if (ablak.ShowDialog() == true)
            {
                ((List<Nevezes>)dgLista.ItemsSource).Add(ablak.Nevezes);
                dgLista.Items.Refresh();
            }
        }
    }
}
