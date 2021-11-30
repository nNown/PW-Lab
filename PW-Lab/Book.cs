using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PW_Lab
{
    public class Book : IGenericItem, INotifyPropertyChanged
    {
        private string _title = new string(string.Empty);
        
        private string _author = new string(string.Empty);

        private int? _readerId = null;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        
        public string Author
        {
            get => _author;
            set
            {
                _author = value;
                OnPropertyChanged();
            }
        }

        public int Id { get; set; }

        public int? ReaderId
        {
            get => _readerId;
            set
            {
                _readerId = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
