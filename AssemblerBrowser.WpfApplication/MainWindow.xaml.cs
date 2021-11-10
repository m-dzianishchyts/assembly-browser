using System.Windows;
using AssemblerBrowser.WpfApplication.ViewModels;

namespace AssemblerBrowser.WpfApplication;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new ApplicationViewModel();
    }
}