using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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

namespace PW_Lab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _sequence;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void LoadSequence(object sender, EventArgs args) {
            var dialog = new OpenFileDialog();
            dialog.Filter = "TXT|*.txt";
            dialog.Title = "Open a sequence file";

            if(dialog.ShowDialog() ?? false)
            {
                _sequence = File.ReadAllText(dialog.FileName);
                UpdateSequenceTextBox(_sequence);
            }
        }

        public void FindPatterns(object sender, EventArgs args)
        {
            var tags = PatternFinder.FindPatterns(_sequence, 4);
            foreach(var tag in tags)
            {
                Tags.Text += $"{tag.Key} {tag.Value}\n";
            }
            UpdateDropDownMenu(tags.Keys);
        }

        public void SelectPattern(object sender, EventArgs args)
            => SelectTag(DropDown.SelectedItem.ToString());

        private void UpdateSequenceTextBox(string sequence) 
            => Sequence.Document.Blocks.Add(new Paragraph(new Run(sequence)));

        private void UpdateDropDownMenu(IEnumerable<string> tags)
        {
            foreach(var tag in tags)
            {
                DropDown.Items.Add(tag);
            }

            DropDown.SelectedItem = DropDown.Items[0];
            SelectTag(DropDown.SelectedItem.ToString());
        }

        private void SelectTag(string tag)
        {
            new TextRange(Sequence.Document.ContentStart, Sequence.Document.ContentEnd).ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);

            var tagRanges = GetAllTagRanges(Sequence.Document, tag);
            foreach(var tagRange in tagRanges)
            {
                if(tagRange.Text == tag)
                {
                    tagRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.BlueViolet);
                }
            }
        }

        private IEnumerable<TextRange> GetAllTagRanges(FlowDocument document, string pattern)
        {
            TextPointer pointer = document.ContentStart;
            while(pointer != null)
            {
                if(pointer.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                {
                    string textRun = pointer.GetTextInRun(LogicalDirection.Forward);
                    MatchCollection matches = Regex.Matches(textRun, pattern);
                    foreach (Match match in matches)
                    {
                        int startIndex = match.Index;
                        int length = match.Length;
                        TextPointer start = pointer.GetPositionAtOffset(startIndex);
                        TextPointer end = pointer.GetPositionAtOffset(startIndex + length);
                        yield return new TextRange(start, end);
                    }
                }

                pointer = pointer.GetNextContextPosition(LogicalDirection.Forward);
            }
        }
    }
}