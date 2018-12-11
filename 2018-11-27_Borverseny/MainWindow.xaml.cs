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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _2018_11_27_Borverseny
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void miBoraszok_Click(object sender, RoutedEventArgs e)
        {
            Boraszok boraszok = new Boraszok();
            boraszok.ShowDialog();
        }

        private void MiNevezesek_Click(object sender, RoutedEventArgs e)
        {
            new Nevezesek().ShowDialog();
        }
    }
}
