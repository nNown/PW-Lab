using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace PW_Lab3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private bool isDataSaved = false;

        private string searchQuery;

        private IEnumerable<Row> originalTableData;

        public ObservableCollection<Row> TableData { get; set; }

        public string SearchQuery
        {
            get => searchQuery;
            set
            {
                searchQuery = value;

                if(TableData.Any())
                    Search(value);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            TableData = new ObservableCollection<Row>();
            TableData.CollectionChanged += (s, e) => isDataSaved = false;
            TableData.CollectionChanged += UpdateCollection;

            originalTableData = new List<Row>();
            searchQuery = string.Empty;

            LoadState();
            DataContext = this;
        }

        public void SaveCSV(object sender, EventArgs args) => SaveCSV();

        public void LoadCSV(object sender, EventArgs args) => LoadCSV();

        public void ConfirmExit(object sender, EventArgs args) => ExitPopup(true);

        public void ExitPopup(object sender, EventArgs args) => ExitPopup();

        public void DeleteItem(object sender, KeyEventArgs args)
        {
            if(args.Key == Key.Delete)
            {
                var grid = (DataGrid)sender;

                if(grid.SelectedItems.Count > 0)
                {
                    foreach(var row in grid.SelectedItems)
                    {
                        ((List<Row>)originalTableData).Remove(row as Row);
                    }

                    RefreshData();
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            StoreState();
            ConfirmExit(e);
            base.OnClosing(e);
        }

        private void UpdateCollection(object sender, EventArgs args)
        {
            foreach (var row in TableData)
            {
                if (!originalTableData.Contains(row))
                {
                    ((List<Row>)originalTableData).Add(row);
                }
            }
        }

        private void SaveCSV()
        {
            SerializeCSV();
            isDataSaved = true;
        }

        private void SerializeCSV()
        {
            XmlSerializer serializer = new XmlSerializer(TableData.GetType());

            var dialog = new SaveFileDialog();
            dialog.Filter = "XML|*.xml";
            dialog.Title = "Save an XML file";
            if (dialog.ShowDialog() ?? false)
            {
                using (FileStream stream = new FileStream(dialog.FileName, FileMode.Create))
                {
                    serializer.Serialize(stream, TableData);
                }
            }
        }

        private void StoreState()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(originalTableData.GetType());

                var directory = Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\state");

                using (FileStream stream = new FileStream(directory.FullName + "\\state.xml", FileMode.Create))
                {
                    serializer.Serialize(stream, originalTableData);
                }
            } catch { }
        }

        private void LoadState()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(originalTableData.GetType());

                using (FileStream stream = new FileStream(Directory.GetCurrentDirectory() + "\\state\\state.xml", FileMode.Open))
                {
                    originalTableData = ((IEnumerable<Row>)serializer.Deserialize(stream)).ToList();
                    RefreshData();
                }
            } catch { }
        }

        private void LoadCSV()
        {
            DeserializeCSV();
            originalTableData = TableData.ToList();
        }

        private void DeserializeCSV()
        {
            XmlSerializer serializer = new XmlSerializer(TableData.GetType());

            TableData.Clear();

            var dialog = new OpenFileDialog();
            dialog.Filter = "XML|*.xml";
            dialog.Title = "Open an XML file";

            if (dialog.ShowDialog() ?? false)
            {
                using (FileStream stream = new FileStream(dialog.FileName, FileMode.Open))
                {
                    IEnumerable<Row> data = (IEnumerable<Row>)serializer.Deserialize(stream);
                    foreach(var row in data)
                    {
                        TableData.Add(row);
                    }
                }
            }
        }

        private void ConfirmExit(CancelEventArgs e)
        {
            if (isDataSaved)
            {
                e.Cancel = false;
                return;
            }

            ExitConfirmation.IsOpen = true;
            e.Cancel = true;
        }

        private void ExitPopup(bool withoutSave = false)
        {
            if (withoutSave)
            {
                isDataSaved = true;
                Close();
            }

            ExitConfirmation.IsOpen = false;
        }

        private void Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                RefreshData();
                return;
            }
            FilterCollection(query);
        }

        private void FilterCollection(string query)
        {
            TableData.Clear();
            try
            {
                Int32.Parse(query);

                foreach(var row in originalTableData)
                    if (row.Count.ToString().Contains(query)) 
                        TableData.Add(row);
            } catch
            {
                foreach (var row in originalTableData)
                    if (row.Name.Contains(query))
                        TableData.Add(row);
            }
        }

        private void RefreshData()
        {
            TableData.Clear();
            foreach (var row in originalTableData)
                TableData.Add(row);
        }
    }
}
