using System;
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
        public string EngineType { get; set; }

        public int Power { get; set; }

        public Engine()
        {
            InitializeComponent();
        }

        private void TypeChecked(object sender, EventArgs args)
        {
            var radioButton = sender as RadioButton;
            EngineType = radioButton?.Content.ToString();
        }

        private void SelectedPower(object sender, EventArgs args)
        {
            Power = Int32.Parse(PowerSelection.Text);
        }

        private void CloseWindow(object sender, EventArgs args)
        {
            Data.Power = Power;
            Data.EngineType = EngineType;

            this.Close();
        }
    }
}
