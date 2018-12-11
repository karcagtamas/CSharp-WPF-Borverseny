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
    /// Interaction logic for BoraszReszletek.xaml
    /// </summary>
    public partial class BoraszReszletek : Window
    {
        public Borasz Borasz { get; set; }
        private int? id = null;
        public BoraszReszletek()
        {
            InitializeComponent();
            this.Borasz = null;
        }
        public BoraszReszletek(Borasz b): this()
        {
            this.id = b.Id;
            txtNev.Text = b.Nev;
            txtEmail.Text = b.Email;
            txtTelefon.Text = b.Telefon;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (KotelezoMezoEllenorzes())
            {
                var b = new Borasz() {
                    Nev = txtNev.Text,
                    Email = txtEmail.Text,
                    Telefon = txtTelefon.Text
                };
                if (id == null)
                {
                    try
                    {
                        this.Borasz = new Adateleres().InsertBorasz(b);
                    }
                    catch (Exception ex)
                    {
                        this.Borasz = null;
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    b.Id = (int)this.id;
                    try
                    {
                        this.Borasz = new Adateleres().UpdateBorasz(b);
                    }
                    catch (Exception ex)
                    {
                        this.Borasz = null;
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
            if (String.IsNullOrEmpty(txtNev.Text))
            {
                txtNev.BorderBrush = Brushes.Red;
                rendben = false;
            }
            if (String.IsNullOrEmpty(txtEmail.Text))
            {
                txtEmail.BorderBrush = Brushes.Red;
                rendben = false;
            }
            return rendben;
        }

        private void btnMegsem_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void txtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).BorderBrush = Brushes.Silver;
        }
    }
}
