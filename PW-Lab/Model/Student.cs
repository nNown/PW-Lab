using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PW_Lab.Model
{
    public class Student : INotifyPropertyChanged
    {
        private string _nameAndSurname;
        private int _id;
        private string _dateAndSignature;

        public string NameAndSurname
        {
            get => _nameAndSurname;
            set
            {
                _nameAndSurname = value;
                OnPropertyChanged();
            }
        }

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string DateAndSignature
        {
            get => _dateAndSignature;
            set
            {
                _dateAndSignature = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
