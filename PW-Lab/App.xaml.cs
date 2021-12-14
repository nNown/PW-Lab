using PW_Lab.Model;
using PW_Lab.View;
using PW_Lab.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace PW_Lab
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var formViewModel = new FormViewModel() { Students = new ObservableCollection<Student>() { new Student() { NameAndSurname = "", DateAndSignature = "", Id = 0 } } };
            var window = new MainWindow(formViewModel);
            window.Show();
        }
    }
}
