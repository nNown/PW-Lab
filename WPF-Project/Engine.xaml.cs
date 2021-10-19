using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WPF_Project
{
    /// <summary>
    /// Interaction logic for Engine.xaml
    /// </summary>
    public partial class Engine : Window
    {
        public ObservableCollection<EquipmentItem> EngineType { get; set; }
        
        public ObservableCollection<EquipmentItem> Power { get; set; }

        public Engine()
        {
            InitializeComponent();
            EngineType = new ObservableCollection<EquipmentItem>() { new EquipmentItem() { Name = "Benzyna", Price = 5000, IsChecked = true }, new EquipmentItem() { Name = "Olej", Price = 7500, IsChecked = false }, new EquipmentItem() { Name = "Gaz", Price = 2000, IsChecked = false }, new EquipmentItem() { Name = "Hybryda", Price = 8000, IsChecked = false } };
            Power = new ObservableCollection<EquipmentItem>() { new EquipmentItem() { Name = "125", Price = 1000, IsChecked = false }, new EquipmentItem() { Name = "150", Price = 2500, IsChecked = false }, new EquipmentItem() { Name = "175", Price = 5000, IsChecked = false }, new EquipmentItem() { Name = "250", Price = 7500, IsChecked = false } };
            this.DataContext = this;

            PriceTextBox.Text = SumPrice().ToString();
        }

        public int SumPrice()
        {
            int price = 0;

            price += EngineType.Where(item => item.IsChecked).FirstOrDefault()?.Price ?? 0;
            if(PowerList.SelectedIndex != -1)
                price += Power[PowerList.SelectedIndex].Price;

            Data.EnginePrice = price;
            MainWindow.Price.Value = Data.EnginePrice + Data.TypePrice;
            return price;
        }

        private void CloseWindow(object sender, EventArgs args)
        {
            Data.EnginePrice = SumPrice();
            this.Close();
        }

        private void PriceChanged(object sender, EventArgs args) => PriceTextBox.Text = SumPrice().ToString();
    }
}
