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
using System.Windows.Threading;

namespace PW_Lab_WMarciniak
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            time.Content = DateTime.Now.ToLongTimeString();
            stoper.Click += openStoper;

            DispatcherTimer dispachterTimer = new DispatcherTimer();
            dispachterTimer.Tick += updateTime;
            dispachterTimer.Interval = new TimeSpan(0, 0, 1);
            dispachterTimer.Start();
        }

        private void updateTime(object sender, EventArgs e)
        {
            time.Content = DateTime.Now.ToLongTimeString();
        }

        private void openStoper(object sender, EventArgs e)
        {
            Stoper stoper = new Stoper();
            stoper.Show();
        }
    }
}
