using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace PW_Lab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Reader> ReadersTable { get; set; } = new ObservableCollection<Reader>();

        public ObservableCollection<Book> BooksTable { get; set; } = new ObservableCollection<Book>();

        public MainWindow()
        {
            InitializeComponent();
            LoadState();
            DataContext = this;
        }

        public void LendBook(object sender, EventArgs args)
        {
            if (!(BooksTableView.SelectedItem is Book book) || !(ReadersTableView.SelectedItem is Reader reader)) return;
            if (book.ReaderId.HasValue) return;

            BooksTable.FirstOrDefault(selectedBook => selectedBook.Id == book.Id).ReaderId = reader.Id;
        }

        public void ReturnBook(object sender, EventArgs args)
        {
            if (!(BooksTableView.SelectedItem is Book book)) return;
            if (!book.ReaderId.HasValue) return;

            BooksTable.FirstOrDefault(selectedBook => selectedBook.Id == book.Id).ReaderId = null;
        }

        public void AddNewReader(object sender, AddingNewItemEventArgs args)
        {
            args.NewItem = new Reader()
            {
                Name = string.Empty,
                Surname = string.Empty,
                Id = GenerateId(ReadersTable)
            };
        }

        public void AddNewBook(object sender, AddingNewItemEventArgs args)
        {
            args.NewItem = new Book()
            {
                Title = string.Empty,
                Author = string.Empty,
                Id = GenerateId(BooksTable),
                ReaderId = null
            };
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            StoreState();
            base.OnClosing(e);
        }

        private int GenerateId(IEnumerable<IGenericItem> collection)
        {
            var random = new Random();
            int Id = random.Next(1, 1000);
            while(collection.Any(item => item.Id == Id))
                Id = random.Next(1, 1000);

            return Id;
        }

        private void StoreState()
        {
            SaveReaders();
            SaveBooks();
        }

        private void LoadState()
        {
            LoadReaders();
            LoadBooks();
        }

        private void SaveReaders()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(ReadersTable.GetType());

                var directory = Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\state");

                using (FileStream stream = new FileStream(directory.FullName + "\\readers.xml", FileMode.Create))
                {
                    serializer.Serialize(stream, ReadersTable);
                }
            }
            catch { }
        }

        private void SaveBooks()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(BooksTable.GetType());

                var directory = Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\state");

                using (FileStream stream = new FileStream(directory.FullName + "\\books.xml", FileMode.Create))
                {
                    serializer.Serialize(stream, BooksTable);
                }
            }
            catch { }
        }

        private void LoadReaders()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(ReadersTable.GetType());

                using (FileStream stream = new FileStream(Directory.GetCurrentDirectory() + "\\state\\readers.xml", FileMode.Open))
                {
                    ReadersTable = (ObservableCollection<Reader>)serializer.Deserialize(stream);
                }
            }
            catch { }
        }

        private void LoadBooks()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(BooksTable.GetType());

                using (FileStream stream = new FileStream(Directory.GetCurrentDirectory() + "\\state\\books.xml", FileMode.Open))
                {
                    BooksTable = (ObservableCollection<Book>)serializer.Deserialize(stream);
                }
            }
            catch { }
        }
    }   
}
