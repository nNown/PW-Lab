using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WPF_Project
{
    /// <summary>
    /// Interaction logic for Type.xaml
    /// </summary>
    public partial class Type : Window
    {
        public ObservableCollection<EquipmentItem> Equipment { get; set; }

        public int Insurance { get; set; }

        public string CarType { get; set; }

        public Type()
        {
            InitializeComponent();
            Equipment = new ObservableCollection<EquipmentItem>() { new EquipmentItem() { Name = "Przedmiot#1", Price = 1000, IsChecked = false }, new EquipmentItem() { Name = "Przedmiot#2", Price = 2000, IsChecked = false }, new EquipmentItem() { Name = "Przedmiot#3", Price = 3000, IsChecked = false } };
            this.DataContext = this;
        }

        private void TypeChecked(object sender, EventArgs args)
        {
            var radioButton = sender as RadioButton;

            if (radioButton == null)
                return;

            CarType = Convert.ToString(radioButton.Content);
        }

        private void CloseWindow(object sender, EventArgs args)
        {
            Data.Equipment = Equipment.Where(item => item.IsChecked).ToList();
            Data.InsurancePrice = Insurance;
            Data.CarType = CarType ?? "Fiat";

            this.Close();
        }
    }
}
