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

namespace PW_Lab
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        public int GridSize { get; set; }

        public string Animal { get; set; }

        public string AnimalURL { get; set; }

        public bool Won { get; set; } 

        public DispatcherTimer dispatcherTimer { get; set; } = new DispatcherTimer();

        public Game(int difficulty, string animal, string animalURL)
        {
            InitializeComponent();
            GridSize = 3 + difficulty;
            Animal = animal;
            AnimalURL = animalURL;

            var rand = new Random();
            var row = rand.Next(0, GridSize);
            var column = rand.Next(0, GridSize);

            for(int i = 0; i < GridSize; i++)
            {
                Buttons.RowDefinitions.Add(new RowDefinition());
                Buttons.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for(int i = 0; i < GridSize; i++)
            {
                for(int j = 0; j < GridSize; j++)
                {
                    var btn = new Button
                    {
                        Width = 75,
                        Height = 75
                    };

                    if (i == row && j == column)
                    {
                        if(animal == "Krokodyl")
                        {
                            btn.Click += (sender, args) =>
                            {
                                Obrazek.Visibility = Visibility.Visible;
                                var died = new Died();
                                died.Show();
                                this.Close();
                            };
                        } else
                        {
                            btn.Click += (sender, args) =>
                            {
                                Obrazek.Visibility = Visibility.Visible;
                                var caught = new Caught(Animal);
                                caught.Show();
                                Won = true;
                            };
                        }
                    }
                    else
                    {
                        btn.Click += (sender, args) =>
                        {
                            btn.Visibility = Visibility.Hidden;
                        };
                    }
                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);
                    Buttons.Children.Add(btn);
                }
            }

            dispatcherTimer.Tick += new EventHandler(Lose);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
            dispatcherTimer.Start();
            DataContext = this;
        }

        private void Lose(object sender, EventArgs args)
        {
            if(!Won)
            {
                var lost = new Lost();
                lost.Show();
                this.Close();
                dispatcherTimer.Stop();
            }
        }
    }
}
