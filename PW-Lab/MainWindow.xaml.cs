using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;

namespace PW_Lab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Airport> Airports { get; set; } = new ObservableCollection<Airport>();

        public MainWindow()
        {
            InitializeComponent();
            LoadAirports();

            DataContext = this;
        }

        private void LoadAirports()
            => LoadCSV();

        private void LoadAiportDetails(object sender, EventArgs args)
        {
            if (AirportsList.SelectedItem is not Airport selectedAirport) return;
            string? ICAO = ICAOBox.IsChecked.Value ? selectedAirport.ICAO : null;
            string? IATA = IATABox.IsChecked.Value ? selectedAirport.IATA : null;
            string? PassengerCount = PassengersBox.IsChecked.Value ? selectedAirport.PassengerCount : null;
            string? Voivodeship = VoivodeshipBox.IsChecked.Value ? selectedAirport.Voivodeship : null;
            string? City = CityBox.IsChecked.Value ? selectedAirport.MainCity : null;
            new AirportDetails(ICAO, IATA, PassengerCount, Voivodeship, City).Show();
        }
        private void LoadCSV()
        {
            Airports.Clear();
            TextFieldParser parser = new TextFieldParser(Directory.GetCurrentDirectory() + "\\Test_Data.csv");
            parser.HasFieldsEnclosedInQuotes = true;
            parser.SetDelimiters(",");
            string[] tokens;

            parser.ReadFields();
            while(!parser.EndOfData)
            {
                tokens = parser.ReadFields();
                Airports.Add(new Airport() { MainCity = tokens[0], Voivodeship = tokens[1], ICAO = tokens[2], IATA = tokens[3], AirportName = tokens[4], PassengerCount = tokens[5], Increase = tokens[6] });
            }

            parser.Close();
        }
    }   
}
