using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PW_Lab
{
    public class Reader : IGenericItem, INotifyPropertyChanged
    {
        private string _name = new string(string.Empty);

        private string _surname = new string(string.Empty);

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }

        public int Id { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
