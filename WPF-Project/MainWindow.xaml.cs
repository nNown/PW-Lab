using System;
using System.Windows;

namespace WPF_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int SummedPrice { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void OpenType(object sender, EventArgs args) => new Type().Show();

        private void OpenEngine(object sender, EventArgs args) => new Engine().Show();

        private void ShowPrice(object sender, EventArgs args) => SumPrice();

        private void SumPrice()
        {
            SummedPrice = 0;

            if (!string.IsNullOrEmpty(Data.CarType)) SummedPrice += 200;
            if (!string.IsNullOrEmpty(Data.EngineType)) SummedPrice += 400;
            SummedPrice += Data.InsurancePrice;
            SummedPrice += Data.Power * 10;
            if(Data.Equipment != null)
                foreach (var item in Data.Equipment)
                    SummedPrice += item.Price;

            price.Text = SummedPrice.ToString();
        }
    }
}
