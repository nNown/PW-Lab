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

namespace PW_Lab
{
    /// <summary>
    /// Interaction logic for Lost.xaml
    /// </summary>
    public partial class Lost : Window
    {
        public Lost()
        {
            InitializeComponent();
        }

        public void Back(object sender, EventArgs args)
        {
            this.Close();
        }
    }
}
