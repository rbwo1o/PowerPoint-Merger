using PowerPoint_Merger.Models;
using PowerPoint_Merger.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace PowerPoint_Merger.Pages;

/// <summary>
/// Interaction logic for HomePage.xaml
/// </summary>
public partial class HomePage : Page
{
    private PowerPointService _ppService = new();

    public HomePage()
    {
        InitializeComponent();
        DataContext = _ppService;
        SearchTextBox.Focus();
    }

    private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        SearchResultsStackPanel.Children.Clear();

        if (string.IsNullOrWhiteSpace(SearchTextBox.Text))
            return;

        IEnumerable<PowerPointModel> matches = _ppService.PPFiles.Where(pp => pp.Name.ToLower().Contains(SearchTextBox.Text.Trim().ToLower()));

        SearchResultsStackPanel.Children.Clear();

        for (int i = 0; i < Math.Min(matches.Count(), 6); i++) 
        {
            // <Button Background = "Azure" Height = "25" Cursor = "Hand" />
            var btn = new System.Windows.Controls.Button 
            {
                Content = matches.ElementAt(i).Name,
                Background = System.Windows.Media.Brushes.Transparent,
                Foreground = System.Windows.Media.Brushes.White,
                Height = 25,
                Cursor = System.Windows.Input.Cursors.Hand
            };
            int elementIndex = i;
            btn.Click += (object sender, RoutedEventArgs e) => SearchResultsButton_Click(sender, e, matches.ElementAt(elementIndex));

            SearchResultsStackPanel.Children.Add(btn);
        }
    }

    void SearchResultsButton_Click(object sender, RoutedEventArgs e, PowerPointModel pp) 
    {
        _ppService.AddTargetPP(pp);
        SearchTextBox.Text = string.Empty;
        SearchTextBox.Focus();
    }

    private void RemoveSelectedTargetPPsButton_Click(object sender, RoutedEventArgs e)
    {
        foreach(var selection in TargetPPsListBox.SelectedItems.Cast<PowerPointModel>().ToList()) 
        {
            _ppService.RemoveTargetPP(selection);
        }
    }

    private void RemoveALLTargetPPsButton_Click(object sender, RoutedEventArgs e)
    {
        foreach (var selection in TargetPPsListBox.Items.Cast<PowerPointModel>().ToList()) 
        {
            _ppService.RemoveTargetPP(selection);
        }
    }
}
