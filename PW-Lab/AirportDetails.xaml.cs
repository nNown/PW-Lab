using System.Collections.Generic;
using System.Windows;

namespace PW_Lab
{
    /// <summary>
    /// Interaction logic for AirportDetails.xaml
    /// </summary>
    public partial class AirportDetails : Window
    {
        public string? ICAOLabel { get; set; }

        public string? IATALabel { get; set; }

        public string? PassengerCountLabel { get; set; }

        public string? VoivodeshipLabel { get; set; }

        public string? CityLabel { get; set; }

        public AirportDetails(string ICAO = null, string IATA = null, string PassengerCount = null, string Voivodeship = null, string City = null)
        {
            InitializeComponent();

            if (ICAO is not null) ICAOLabel = "ICAO: " + ICAO;
            if (IATA is not null) IATALabel = "IATA: " + IATA;
            if (PassengerCount is not null) PassengerCountLabel = "Liczba pasażerów: " + PassengerCount;
            if (Voivodeship is not null) VoivodeshipLabel = "Województwo: " + Voivodeship;
            if (City is not null) CityLabel = "Miasto: " + City;

            DataContext = this;
        }
    }
}
