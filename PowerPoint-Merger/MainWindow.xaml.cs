using PowerPoint_Merger.Pages;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PowerPoint_Merger;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainFrame.Navigate(new HomePage());
        MainFrame.Focus();
    }

    private void SourcesMenuItem_Click(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(new SourcesPage());
    }

    private void HomeMenuItem_Click(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(new HomePage());
    }

    private void InfoMenuItem_Click(object sender, RoutedEventArgs e)
    {
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
            FileName = "https://github.com/rbwo1o/PowerPoint-Merger",
            UseShellExecute = true
        });
    }
}