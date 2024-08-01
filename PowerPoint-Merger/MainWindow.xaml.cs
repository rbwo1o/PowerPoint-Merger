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
        MainFrame.Navigated += MainFrame_Navigated;
    }

    private void MainFrame_Navigated(object sender, NavigationEventArgs e)
    {
        NotificationStackPanel.Children.Clear();

        if (e.Content is HomePage)
        {
            var linkTextBlock = new System.Windows.Controls.TextBlock();
            linkTextBlock.Text = $"Output Directory '{App._ConfigurationService.Configuration.OutputDirectory}'";
            linkTextBlock.Background = System.Windows.Media.Brushes.Transparent;
            linkTextBlock.Foreground = System.Windows.Media.Brushes.LightBlue;
            linkTextBlock.Height = 30;
            linkTextBlock.FontFamily = new System.Windows.Media.FontFamily("Segoe UI");
            linkTextBlock.TextDecorations = TextDecorations.Underline;
            linkTextBlock.Cursor = System.Windows.Input.Cursors.Hand;
            linkTextBlock.MouseLeftButtonDown += OutputDirectoryMenuItem_Click;
            linkTextBlock.Margin = new System.Windows.Thickness(5, 6, 0, 0);

            NotificationStackPanel.Children.Add(linkTextBlock);
        }
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

    private void OutputDirectoryMenuItem_Click(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(new OutputDirectoryPage());
    }
}