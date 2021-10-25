using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using Microsoft.Win32;

namespace PW_Lab3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public ObservableCollection<Row> TableData { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            TableData = new ObservableCollection<Row>();

            DataContext = this;
        }

        public void SaveCSV(object sender, EventArgs args) => SaveCSV();

        public void LoadCSV(object sender, EventArgs args) => LoadCSV();

        private void SaveCSV()
        {
            var sb = new StringBuilder();

            var headers = DataGrid.Columns.Cast<DataGridColumn>();
            sb.AppendLine(string.Join(",", headers.Select(column => column.Header).ToArray()));

            foreach(var row in DataGrid.Items)
            {
                if(row is Row rowData) 
                {
                    sb.AppendLine(string.Join(",", rowData.Name, rowData.Id, rowData.Count));
                }
            }

            var dialog = new SaveFileDialog();
            dialog.Filter = "CSV|*.csv";
            dialog.Title = "Save an CSV file";

            if(dialog.ShowDialog() ?? false)
            {
                using(StreamWriter sw = new StreamWriter(dialog.FileName))
                {
                    sw.Write(sb.ToString());
                }
            }
        }

        private void LoadCSV()
        {
            TableData.Clear();

            var dialog = new OpenFileDialog();
            dialog.Filter = "CSV|*.csv";
            dialog.Title = "Open an CSV file";

            if(dialog.ShowDialog() ?? false)
            {
                using(StreamReader sr = new StreamReader(dialog.FileName))
                {
                    sr.ReadLine();
                    while(sr.ReadLine() is string line)
                    {
                        var lineContent = line.Split(",");
                        TableData.Add(new Row() { Name = lineContent[0], Id = lineContent[1], Count = Int32.Parse(lineContent[2]) });
                    }
                }
            }
        }
    }
}
