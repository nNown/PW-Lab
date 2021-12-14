using System.Collections.ObjectModel;

namespace PW_Lab.Model
{
    public class Form
    {
        public string University { get; set; }

        public string Course { get; set; }

        public string Degree { get; set; }

        public string Profile { get; set; }

        public string CourseType { get; set; }

        public string CourseLevel { get; set; }

        public ObservableCollection<Student> Students { get; set; }

        public string Title { get; set; }

        public string EnglishTitle { get; set; }

        public string InputData { get; set; }

        public string Scope { get; set; }

        public string DeliveryDate { get; set; }

        public string Promoter { get; set; }

        public string PromoterUnit { get; set; }
    }
}
