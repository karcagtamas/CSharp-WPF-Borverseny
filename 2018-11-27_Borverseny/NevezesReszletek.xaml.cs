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
    /// Interaction logic for NevezesReszletek.xaml
    /// </summary>
    public partial class NevezesReszletek : Window
    {
        public Nevezes Nevezes { get; set; }
        private int? id = null;
        public NevezesReszletek()
        {
            InitializeComponent();
            List<Kategoria> lista = new Adateleres().ListKategoriak();
            cboKategoria.ItemsSource = lista;
            cboKategoria.DisplayMemberPath = "Megnevezes";
            cboKategoria.SelectedValuePath = "Id";

            cboBoraszok.ItemsSource = new Adateleres().ListBoraszok("");
            cboBoraszok.DisplayMemberPath = "Nev";
            cboBoraszok.SelectedValuePath = "Id";

            this.Nevezes = null;
        }

        public NevezesReszletek(Nevezes b): this()
        {
            this.id = b.Id;
            txtBorvidek.Text = b.Borvidek;
            txtEvjarat.Text = b.Evjarat.ToString();
            txtFantazianev.Text = b.FantaziaNev;
            txtHelyezes.Text = b.Helyezes.ToString();
            cboBoraszok.SelectedValue = b.BoraszId;
            cboKategoria.SelectedValue = b.KategoriaId;
        }

        private void btnMegsem_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnMentes_Click(object sender, RoutedEventArgs e)
        {
            if (KotelezoMezoEllenorzes())
            {
                var n = new Nevezes()
                {
                    Borvidek = txtBorvidek.Text,
                    Evjarat = Convert.ToInt32(txtEvjarat.Text),
                    FantaziaNev = txtFantazianev.Text,
                    Helyezes = String.IsNullOrEmpty(txtHelyezes.Text)? null : (int?)Convert.ToInt32(txtHelyezes.Text),
                    BoraszId = (int)cboBoraszok.SelectedValue,
                    KategoriaId = (int)cboKategoria.SelectedValue
                };
                if (id == null)
                {
                    try
                    {
                        this.Nevezes = new Adateleres().InsertNevezes(n);
                    }
                    catch (Exception ex)
                    {
                        this.Nevezes = null;
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    n.Id = (int)this.id;
                    try
                    {
                        this.Nevezes = new Adateleres().UpdateNevezes(n);
                    }
                    catch (Exception ex)
                    {
                        this.Nevezes = null;
                        MessageBox.Show(ex.Message);
                    }
                }
                this.DialogResult = true;
                this.Close();
            }
        }

        private bool KotelezoMezoEllenorzes()
        {
            bool rendben = true;
            if (String.IsNullOrEmpty(txtBorvidek.Text))
            {
                txtBorvidek.BorderBrush = Brushes.Red;
                rendben = false;
            }
            if (String.IsNullOrEmpty(txtFantazianev.Text))
            {
                txtFantazianev.BorderBrush = Brushes.Red;
                rendben = false;
            }
            if (String.IsNullOrEmpty(txtEvjarat.Text))
            {
                txtEvjarat.BorderBrush = Brushes.Red;
                rendben = false;
            }
            else
            {
                try
                {
                    int x = int.Parse(txtEvjarat.Text);
                }
                catch
                {
                    txtEvjarat.BorderBrush = Brushes.Red;
                    rendben = false;
                }
            }
            if (!String.IsNullOrEmpty(txtHelyezes.Text))
            {
                try
                {
                    int x = int.Parse(txtHelyezes.Text);
                }
                catch
                {
                    txtHelyezes.BorderBrush = Brushes.Red;
                    rendben = false;
                }
            }
            if (cboBoraszok.SelectedIndex == -1)
            {
                cboBoraszok.BorderBrush = Brushes.Red;
                rendben = false;
            }
            if (cboKategoria.SelectedIndex == -1)
            {
                cboKategoria.BorderBrush = Brushes.Red;
                rendben = false;
            }
            return rendben;
        }
        private void txtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).BorderBrush = Brushes.Silver;
        }
    }
}
