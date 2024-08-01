using PowerPoint_Merger.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using Media = System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PowerPoint_Merger.Windows
{
    /// <summary>
    /// Interaction logic for SourceWindow.xaml
    /// </summary>
    public partial class SourceWindow : Window
    {
        private SourceModel? _source = null;
        private string _errorTextEmpty = "This is a required field!";
        private string _errorTextDuplicate = "This source directory already exists!";

        public SourceWindow(SourceModel? source)
        {
            InitializeComponent();

            _source = source;
            
            if (_source == null)
                Title = "Add Source Directory";
            else 
            {
                Title = $"Edit {_source.Name}";
                NameTextBox.Text = _source.Name;
                SourceTextBox.Text = _source.Path;
            }

            NameTextBox.Focus();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SelectFolderButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select a folder as a Source";
                dialog.ShowNewFolderButton = true;

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    SourceTextBox.Text = dialog.SelectedPath;

                    SourceTextBox.Background = new Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#ADADAD"));
                    SourceTextBox.FontWeight = FontWeights.Normal;
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // REQUIRE SOURCE
            if (!String.IsNullOrWhiteSpace(SourceTextBox.Text) && SourceTextBox.Text != _errorTextEmpty && SourceTextBox.Text != _errorTextDuplicate) 
            {
                if (_source == null) // Add 
                {
                    if (App._ConfigurationService.Configuration.Sources.Find(x => x.Path == SourceTextBox.Text) != null)
                    {
                        SourceTextBox.Text = _errorTextDuplicate;
                        SourceTextBox.Background = System.Windows.Media.Brushes.Red;

                        SelectFolderButton.Focus();
                        return;
                    }

                    _source = new SourceModel
                    {
                        Name = NameTextBox.Text,
                        Path = SourceTextBox.Text,
                        RecursivlySearch = Recursive_CheckBox.IsChecked!.Value
                    };

                    App._ConfigurationService.Configuration.Sources.Add(_source);
                }
                else // Edit
                {
                    int index = App._ConfigurationService.Configuration.Sources.FindIndex(x => x.Path == _source.Path);

                    App._ConfigurationService.Configuration.Sources[index].Name = NameTextBox.Text;
                    App._ConfigurationService.Configuration.Sources[index].Path = SourceTextBox.Text;
                    App._ConfigurationService.Configuration.Sources[index].RecursivlySearch = Recursive_CheckBox.IsChecked!.Value;
                }

                // Save
                App._ConfigurationService.Save();
                Close();
            }
            else
            {
                SourceTextBox.Text = _errorTextEmpty;
                SourceTextBox.Background = new Media.SolidColorBrush((Media.Color)Media.ColorConverter.ConvertFromString("#FF474D"));

                SelectFolderButton.Focus();
            }
        }
    }
}
