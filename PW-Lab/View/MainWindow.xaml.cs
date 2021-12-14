using Microsoft.Win32;
using PW_Lab.ViewModel;
using System;
using System.ComponentModel;
using System.Windows;

namespace PW_Lab.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FormViewModel _viewModel;

        private bool _canExit = false;

        public MainWindow(FormViewModel viewModel)
        {
            _viewModel = viewModel;
            DataContext = viewModel;

            Initialized += LoadData;
            InitializeComponent();
        }

        public void LoadData(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "XML|*.xml";
            dialog.Title = "Open an XML file";

            if (dialog.ShowDialog() ?? false)
            {
                _viewModel.LoadData(dialog.FileName);
            }
        }

        public void SaveDataAndExit(object sender, EventArgs e) => ExitPopup(true);

        public void ExitWithoutSavingData(object sender, EventArgs e) => ExitPopup();

        protected override void OnClosing(CancelEventArgs e)
        {
            ConfirmExit(e);
            base.OnClosing(e);
        }

        private void ConfirmExit(CancelEventArgs e)
        {
            if (_canExit)
            {
                e.Cancel = false;
                return;
            }

            SavePopup.IsOpen = true;
            e.Cancel = true;
        }

        private void ExitPopup(bool withSave = false)
        {
            if (withSave)
            {
                var dialog = new OpenFileDialog();
                dialog.Filter = "XML|*.xml";
                dialog.Title = "Open an XML file";

                if (dialog.ShowDialog() ?? false)
                {
                    _viewModel.SaveData(dialog.FileName);
                }
                Close();
            }

            SavePopup.IsOpen = false;
            _canExit = true;
            Close();
        }
    }
}
