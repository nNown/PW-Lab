using System;
using System.Windows;

namespace WPF_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ObservablePrice Price { get; set; } = new ObservablePrice();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void OpenType(object sender, EventArgs args) => new Type().Show();

        private void OpenEngine(object sender, EventArgs args) => new Engine().Show();
    }
}
