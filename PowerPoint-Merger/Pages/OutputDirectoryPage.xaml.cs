using System;
using System.Collections.Generic;
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

namespace PowerPoint_Merger.Pages
{
    /// <summary>
    /// Interaction logic for OutputDirectoryPage.xaml
    /// </summary>
    public partial class OutputDirectoryPage : Page
    {
        public OutputDirectoryPage()
        {
            InitializeComponent();
            DataContext = App._ConfigurationService.Configuration;
        }

        private void SelectOutputDirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select a folder as a Source";
                dialog.ShowNewFolderButton = true;

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    OutputDirectoryTextBox.Text = dialog.SelectedPath;
                    App._ConfigurationService.Configuration.OutputDirectory = dialog.SelectedPath;
                    App._ConfigurationService.Save();
                }
            }
        }
    }
}
