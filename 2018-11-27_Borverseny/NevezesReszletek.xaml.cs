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
        }
    }
}
