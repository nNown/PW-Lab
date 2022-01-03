using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace PW_Lab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MediaPlayer _mediaPlayer = new MediaPlayer();

        public ObservableCollection<string> MusicFiles { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\songs");
            MusicFiles = new ObservableCollection<string>(files);
            DataContext = this;
        }

        private void PlaySong(object sender, EventArgs args)
        {
            _mediaPlayer.Open(new Uri((string)MusicList.SelectedItem));
            _mediaPlayer.Play();
        }

        private void StopSong(object sender, EventArgs args)
        {
            _mediaPlayer.Stop();
        }
    }
}
