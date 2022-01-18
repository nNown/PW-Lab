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
    /// Interaction logic for Caught.xaml
    /// </summary>
    public partial class Caught : Window
    {
        public Caught(string animal)
        {
            InitializeComponent();
            TekstWygranej.Content = "Złapałeś " + animal;
            DataContext = this;
        }

        public void Back(object sender, EventArgs args)
        {
            this.Close();
        }
    }
}
