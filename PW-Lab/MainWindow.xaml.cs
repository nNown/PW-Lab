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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PW_Lab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> Animals { get; set; } = new ObservableCollection<string>() { "Mysz", "Ryba", "Kot", "Krokodyl" };

        public ObservableCollection<string> Difficulty { get; set; } = new ObservableCollection<string>() { "Łatwy", "Średni", "Trudny" };

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void StartGame(object sender, EventArgs args)
        {
            var difficulty = DifficultySelection.SelectedItem.ToString();
            var animal = AnimalsSelection.SelectedItem.ToString();
            Game game;
            switch(difficulty)
            {
                case "Łatwy":
                    game = new Game(0, animal, AnimalURL(animal));
                    game.Show();
                    break;
                case "Średni":
                    game = new Game(1, animal, AnimalURL(animal));
                    game.Show();
                    break;
                case "Trudny":
                    game = new Game(2, animal, AnimalURL(animal));
                    game.Show();
                    break;
            }
        }

        private string AnimalURL(string selectedAnimal)
        {
            if (selectedAnimal == "Mysz") return "https://upload.wikimedia.org/wikipedia/commons/thumb/4/42/%D0%9C%D1%8B%D1%88%D1%8C_3.jpg/1200px-%D0%9C%D1%8B%D1%88%D1%8C_3.jpg";
            else if (selectedAnimal == "Ryba") return "https://zasoby.ekologia.pl/artykulyNew/27426/xxl/shutterstock-1408036886_800x600.jpg";
            else if (selectedAnimal == "Kot") return "https://www.zooplus.pl/magazyn/wp-content/uploads/2019/12/kot-przyb%C5%82%C4%99da-768x512.jpeg";
            else return "https://fajnepodroze.pl/wp-content/uploads/2020/02/krokodyl.jpg";
        }
    }
}
