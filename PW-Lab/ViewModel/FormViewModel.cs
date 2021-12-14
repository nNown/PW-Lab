using PW_Lab.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace PW_Lab.ViewModel
{
    public class FormViewModel : INotifyPropertyChanged
    {
        public Form Model { get; set; }

        public string University
        {
            get => Model.University;
            set
            {
                Model.University = value;
                OnPropertyChanged();
            }
        }

        public string Course
        {
            get => Model.Course;
            set
            {
                Model.Course = value;
                OnPropertyChanged();
            }
        }

        public string Degree
        {
            get => Model.Degree;
            set
            {
                Model.Degree = value;
                OnPropertyChanged();
            }
        }

        public string Profile
        {
            get => Model.Profile;
            set
            {
                Model.Profile = value;
                OnPropertyChanged();
            }
        }

        public string CourseType
        {
            get => Model.CourseType;
            set
            {
                Model.CourseType = value;
                OnPropertyChanged();
            }
        }

        public string CourseLevel
        {
            get => Model.CourseLevel;
            set
            {
                Model.CourseLevel = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Student> Students
        {
            get => Model.Students;
            set
            {
                Model.Students = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => Model.Title;
            set
            {
                Model.Title = value;
                OnPropertyChanged();
            }
        }

        public string EnglishTitle
        {
            get => Model.EnglishTitle;
            set
            {
                Model.EnglishTitle = value;
                OnPropertyChanged();
            }
        }

        public string InputData
        {
            get => Model.InputData;
            set
            {
                Model.InputData = value;
                OnPropertyChanged();
            }
        }

        public string Scope
        {
            get => Model.Scope;
            set
            {
                Model.Scope = value;
                OnPropertyChanged();
            }
        }

        public string DeliveryDate
        {
            get => Model.DeliveryDate;
            set
            {
                Model.DeliveryDate = value;
                OnPropertyChanged();
            }
        }

        public string Promoter
        {
            get => Model.Promoter;
            set
            {
                Model.Promoter = value;
                OnPropertyChanged();
            }
        }

        public string PromoterUnit
        {
            get => Model.PromoterUnit;
            set
            {
                Model.PromoterUnit = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public FormViewModel()
        {
            Model = new Form();
        }

        public FormViewModel(string University = "", string Course = "", string Degree = "", string Profile = "", string CourseType = "", string CourseLevel = "", ObservableCollection<Student> Students = null, string Title = "", string EnglishTitle = "", string InputData = "", string Scope = "", string DeliveryDate = "", string Promoter = "", string PromoterUnit = "")
        {
            Model = new Form() { University = University, Course = Course, Degree = Degree, Profile = Profile, CourseType = CourseType, CourseLevel = CourseLevel, Students = Students, Title = Title, EnglishTitle = EnglishTitle, InputData = InputData, Scope = Scope, DeliveryDate = DeliveryDate, Promoter = Promoter, PromoterUnit = PromoterUnit };
        }

        public void SaveData(string path)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(this.GetType());
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    serializer.Serialize(stream, this);
                }
            } catch { }
        }

        public void LoadData(string path)
        {
            var loadedViewModel = new FormViewModel();
            try
            {
                XmlSerializer serializer = new XmlSerializer(this.GetType());

                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    loadedViewModel = (FormViewModel)serializer.Deserialize(stream);
                }
            } catch { }

            this.University = loadedViewModel.University;
            this.Course = loadedViewModel.Course;
            this.Degree = loadedViewModel.Degree;
            this.Profile = loadedViewModel.Profile;
            this.CourseType = loadedViewModel.CourseType;
            this.CourseLevel = loadedViewModel.CourseLevel;
            this.Students = loadedViewModel.Students;
            this.Title = loadedViewModel.Title;
            this.EnglishTitle = loadedViewModel.EnglishTitle;
            this.InputData = loadedViewModel.InputData;
            this.Scope = loadedViewModel.Scope;
            this.DeliveryDate = loadedViewModel.DeliveryDate;
            this.Promoter = loadedViewModel.Promoter;
            this.PromoterUnit = loadedViewModel.PromoterUnit;
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
