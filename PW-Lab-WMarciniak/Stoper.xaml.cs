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
using System.Windows.Threading;

namespace PW_Lab_WMarciniak
{
    /// <summary>
    /// Interaction logic for Stoper.xaml
    /// </summary>
    public partial class Stoper : Window
    {
        private DispatcherTimer _stoperDispatcher;
        private TimeSpan _time;
        public Stoper()
        {
            InitializeComponent();
            stoper.Content = _time.Duration().ToString();

            _stoperDispatcher = new DispatcherTimer();
            _stoperDispatcher.Tick += updateTime;
            _stoperDispatcher.Interval = new TimeSpan(TimeSpan.TicksPerMillisecond);

            start.Click += startStoper;
            stop.Click += (o, e) => _stoperDispatcher.Stop();
        }

        private void updateTime(object sender, EventArgs e)
        {
            _time += new TimeSpan(TimeSpan.TicksPerMillisecond);
            stoper.Content = _time.Duration().ToString();
        }

        private void startStoper(object sender, EventArgs e)
        {
            _time = new TimeSpan();
            stoper.Content = _time.Duration().ToString();
            _stoperDispatcher.Start();
        }
    }
}
