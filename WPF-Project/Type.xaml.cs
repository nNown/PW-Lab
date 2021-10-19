using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPF_Project
{
    /// <summary>
    /// Interaction logic for Type.xaml
    /// </summary>
    public partial class Type : Window
    {
        public ObservableCollection<EquipmentItem> Equipment { get; set; }

        public ObservableCollection<EquipmentItem> CarType { get; set; }

        public Type()
        {
            InitializeComponent();
            Equipment = new ObservableCollection<EquipmentItem>() { new EquipmentItem() { Name = "Gaśnica", Price = 1000, IsChecked = false }, new EquipmentItem() { Name = "Kocyk", Price = 2000, IsChecked = false }, new EquipmentItem() { Name = "Pluszowy miś", Price = 3000, IsChecked = false } };
            CarType = new ObservableCollection<EquipmentItem>() { new EquipmentItem() { Name = "Fiat", Price = 20000, IsChecked = true }, new EquipmentItem() { Name = "Ford", Price = 25000, IsChecked = false }, new EquipmentItem() { Name = "Ferrarri", Price = 100000, IsChecked = false } };

            PriceTextBox.Text = SumPrice().ToString();

            this.DataContext = this;
        }

        public int SumPrice()
        {
            int price = 0;

            foreach (var item in Equipment.Where(item => item.IsChecked).ToList())
                price += item.Price;

            price += CarType.Where(item => item.IsChecked).FirstOrDefault().Price;
            try
            {
                price += Int32.Parse(InsuranceTextBox.Text);
            } catch { }

            Data.TypePrice = price;
            MainWindow.Price.Value = Data.EnginePrice + Data.TypePrice;
            return price;
        }

        private void CloseWindow(object sender, EventArgs args)
        {
            Data.TypePrice = SumPrice();
            this.Close();
        }

        private void PriceChanged(object sender, EventArgs args) => PriceTextBox.Text = SumPrice().ToString();
    }
}
