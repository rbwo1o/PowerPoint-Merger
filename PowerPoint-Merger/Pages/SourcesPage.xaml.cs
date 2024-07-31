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
using PowerPoint_Merger.Windows;
using PowerPoint_Merger.Models;

namespace PowerPoint_Merger.Pages;

/// <summary>
/// Interaction logic for SourcesPage.xaml
/// </summary>
public partial class SourcesPage : Page
{
    public SourcesPage()
    {
        InitializeComponent();
        SourcesDataGrid.ItemsSource = App._Configuration.Sources;
        EditSourceButton.IsEnabled = false;
        RemoveSourceButton.IsEnabled = false;
    }

    private void SourcesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (SourcesDataGrid.SelectedItem != null)
        {
            EditSourceButton.IsEnabled = true;
            RemoveSourceButton.IsEnabled = true;
        }
        else 
        {
            EditSourceButton.IsEnabled = false;
            RemoveSourceButton.IsEnabled = false;
        }
    }

    private void AddSourceButton_Click(object sender, RoutedEventArgs e)
    {
        SourceWindow sourceWindow = new(null);
        sourceWindow.Closed += SourceWindow_Closed!;
        sourceWindow.ShowDialog();
    }

    private void EditSourceButton_Click(object sender, RoutedEventArgs e)
    {
        SourceWindow sourceWindow = new((SourceModel)SourcesDataGrid.SelectedItem);
        sourceWindow.Closed += SourceWindow_Closed!;
        sourceWindow.ShowDialog();
    }

    private void SourceWindow_Closed(object sender, System.EventArgs e) 
    {
        SourcesDataGrid.Items.Refresh();
    }

    private void RemoveSourceButton_Click(object sender, RoutedEventArgs e)
    {
        App._Configuration.Sources!.Remove((SourceModel)SourcesDataGrid.SelectedItem);
        App._Configuration.Save();

        SourcesDataGrid.Items.Refresh();
    }
}
