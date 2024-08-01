using System.Configuration;
using System.Data;
using System.Windows;
using PowerPoint_Merger.Services;

namespace PowerPoint_Merger;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    public static ConfigurationService _ConfigurationService = new ConfigurationService();

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        if (!_ConfigurationService.Initialize())
        {
            // display error
            // potentially create a new config file
        }
        else 
        {
            MainWindow window = new MainWindow();
            window.Show();
        }
        
    }
}
