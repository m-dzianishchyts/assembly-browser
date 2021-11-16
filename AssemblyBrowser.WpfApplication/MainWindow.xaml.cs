using System.Windows;
using AssemblyBrowser.WpfApplication.ViewModels;

namespace AssemblyBrowser.WpfApplication;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new ApplicationViewModel();
    }
}